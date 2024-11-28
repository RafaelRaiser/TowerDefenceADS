using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb; // Referência ao Rigidbody2D do projétil
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do projétil
    [SerializeField] protected int dano = 10; // Dano base do projétil

    protected Transform target; // Alvo que o projétil está perseguindo
    private bool jaAplicouDano = false; // Garante que o dano seja aplicado uma única vez

    // Atributos específicos para dano de fogo
    private bool isFireBullet = false; // Define se esta é uma bala de fogo
    private float fireDamagePerSecond; // Dano por segundo de fogo
    private float burnDuration; // Duração do efeito de fogo

    // Método para definir o alvo do projétil
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Método para configurar o dano de fogo
    public void SetFireDamage(float damagePerSecond, float duration)
    {
        isFireBullet = true; // Marca a bala como sendo de fogo
        fireDamagePerSecond = damagePerSecond;
        burnDuration = duration;
    }

    // Atualiza a física do projétil
    protected virtual void FixedUpdate()
    {
        if (!target) return; // Sai se não há alvo

        // Calcula a direção para o alvo
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade na direção do alvo
        rb.velocity = direction * bulletSpeed;
    }

    // Método chamado quando o projétil colide com outro objeto
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (jaAplicouDano) return; // Garante que o dano é aplicado apenas uma vez
        jaAplicouDano = true;

        // Verifica se o objeto colidido implementa a interface IReceberDano
        IReceberDano inimigo = collision.GetComponent<IReceberDano>();
        if (inimigo != null)
        {
            if (isFireBullet)
            {
                // Aplica o efeito de fogo
                StartCoroutine(ApplyBurnDamage(inimigo));
            }
            else
            {
                // Aplica dano instantâneo
                inimigo.ReceberDano(dano);
            }
        }

        // Destrói o projétil
        Destroy(gameObject);
    }

    // Coroutine para aplicar dano contínuo por fogo
    private IEnumerator ApplyBurnDamage(IReceberDano inimigo)
    {
        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            // Aplica o dano de fogo por segundo ao longo da duração
            inimigo.ReceberDano(fireDamagePerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Define a direção manualmente (para balas sem alvo específico)
    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do proj�til
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do proj�til
    [SerializeField] protected int dano = 10; // Dano base do proj�til

    protected Transform target; // Alvo que o proj�til est� perseguindo
    private bool jaAplicouDano = false; // Garante que o dano seja aplicado uma �nica vez

    // Atributos espec�ficos para dano de fogo
    private bool isFireBullet = false; // Define se esta � uma bala de fogo
    private float fireDamagePerSecond; // Dano por segundo de fogo
    private float burnDuration; // Dura��o do efeito de fogo

    // M�todo para definir o alvo do proj�til
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // M�todo para configurar o dano de fogo
    public void SetFireDamage(float damagePerSecond, float duration)
    {
        isFireBullet = true; // Marca a bala como sendo de fogo
        fireDamagePerSecond = damagePerSecond;
        burnDuration = duration;
    }

    // Atualiza a f�sica do proj�til
    protected virtual void FixedUpdate()
    {
        if (!target) return; // Sai se n�o h� alvo

        // Calcula a dire��o para o alvo
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade na dire��o do alvo
        rb.velocity = direction * bulletSpeed;
    }

    // M�todo chamado quando o proj�til colide com outro objeto
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (jaAplicouDano) return; // Garante que o dano � aplicado apenas uma vez
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
                // Aplica dano instant�neo
                inimigo.ReceberDano(dano);
            }
        }

        // Destr�i o proj�til
        Destroy(gameObject);
    }

    // Coroutine para aplicar dano cont�nuo por fogo
    private IEnumerator ApplyBurnDamage(IReceberDano inimigo)
    {
        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            // Aplica o dano de fogo por segundo ao longo da dura��o
            inimigo.ReceberDano(fireDamagePerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Define a dire��o manualmente (para balas sem alvo espec�fico)
    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }
}

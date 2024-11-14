using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe para uma torre de fogo, que aplica um efeito de queimadura ao alvo
public class TurretFire : Turret
{
    [SerializeField] private float burnDuration = 3f; // Duração do efeito de queimadura.
    [SerializeField] private float burnDamagePerSecond = 1f; // Dano por segundo da queimadura.

    // Método que ataca o alvo aplicando dano ao longo do tempo (efeito de queimadura)
    public override void Atacar()
    {
        if (target != null) // Verifica se há um alvo.
        {
            Health enemyHealth = target.GetComponent<Health>(); // Obtém o componente de saúde do inimigo.
            if (enemyHealth != null)
            {
                StartCoroutine(ApplyBurnDamage(enemyHealth)); // Inicia o efeito de queimadura no inimigo.
            }
        }
    }

    // Coroutine que aplica dano contínuo ao inimigo durante a duração do efeito
    private IEnumerator ApplyBurnDamage(Health enemyHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < burnDuration) // Enquanto o tempo não exceder a duração do efeito
        {
            enemyHealth.Damaged(burnDamagePerSecond * Time.deltaTime); // Aplica dano a cada quadro.
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null; // Espera um quadro antes de repetir o loop.
        }
    }

    // Método para disparar projétil e também iniciar o ataque (efeito de queimadura)
    protected override void Shoot()
    {
        // Instancia um projétil e define seu alvo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        Atacar(); // Inicia o ataque com o efeito de queimadura.
    }
}

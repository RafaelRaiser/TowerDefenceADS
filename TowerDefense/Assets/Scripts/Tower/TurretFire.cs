using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe para uma torre de fogo, que aplica um efeito de queimadura ao alvo
public class TurretFire : Turret
{
    [SerializeField] private float burnDuration = 3f; // Dura��o do efeito de queimadura.
    [SerializeField] private float burnDamagePerSecond = 1f; // Dano por segundo da queimadura.

    // M�todo que ataca o alvo aplicando dano ao longo do tempo (efeito de queimadura)
    public override void Atacar()
    {
        if (target != null) // Verifica se h� um alvo.
        {
            Health enemyHealth = target.GetComponent<Health>(); // Obt�m o componente de sa�de do inimigo.
            if (enemyHealth != null)
            {
                StartCoroutine(ApplyBurnDamage(enemyHealth)); // Inicia o efeito de queimadura no inimigo.
            }
        }
    }

    // Coroutine que aplica dano cont�nuo ao inimigo durante a dura��o do efeito
    private IEnumerator ApplyBurnDamage(Health enemyHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < burnDuration) // Enquanto o tempo n�o exceder a dura��o do efeito
        {
            enemyHealth.Damaged(burnDamagePerSecond * Time.deltaTime); // Aplica dano a cada quadro.
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null; // Espera um quadro antes de repetir o loop.
        }
    }

    // M�todo para disparar proj�til e tamb�m iniciar o ataque (efeito de queimadura)
    protected override void Shoot()
    {
        // Instancia um proj�til e define seu alvo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        Atacar(); // Inicia o ataque com o efeito de queimadura.
    }
}

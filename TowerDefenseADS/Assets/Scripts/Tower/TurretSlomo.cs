using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


// Torre que aplica um efeito de desaceleração nos inimigos no alcance
public class TurretSlomo : Turret, Iatacavel
{
    [SerializeField] private float aps = 4f; // Ataques por segundo.
    [SerializeField] private float FreezeTime = 1f; // Duração do efeito de congelamento.

    private void Update()
    {
        timeUntilFire += Time.deltaTime; // Acumula o tempo até o próximo ataque.

        if (timeUntilFire >= 1f / aps) // Verifica se é hora de atacar.
        {
            FreezeEnemies(); // Aplica o efeito de congelamento nos inimigos.
            timeUntilFire = 0f; // Reseta o tempo até o próximo ataque.
        }
    }

    // Método que aplica o efeito de congelamento nos inimigos no alcance
    private void FreezeEnemies()
    {
        // Realiza um CircleCast para detectar inimigos no alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            EnemyMover em = hit.transform.GetComponent<EnemyMover>();
            if (em != null)
            {
                em.UpdateSpeed(0.5f); // Reduz a velocidade do inimigo.
                StartCoroutine(ResetEnemySpeed(em)); // Reseta a velocidade após o efeito.
            }
        }
    }

    // Coroutine que restaura a velocidade do inimigo após o tempo de efeito
    private IEnumerator ResetEnemySpeed(EnemyMover em)
    {
        yield return new WaitForSeconds(FreezeTime); // Espera o tempo de congelamento.
        em.ResetSpeed(); // Restaura a velocidade original do inimigo.
    }
}

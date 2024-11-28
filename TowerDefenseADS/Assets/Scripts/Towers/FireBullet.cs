using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaDeFogo : Bullet
{
    private float fireDamagePerSecond; // Dano por segundo
    private float burnDuration; // Duração do efeito de queima

    // Configura os atributos específicos de dano de fogo
    public void SetFireDamage(float damagePerSecond, float duration)
    {
        fireDamagePerSecond = damagePerSecond;
        burnDuration = duration;
    }

    // Sobrescreve o comportamento ao atingir o alvo
    protected override void OnHitTarget()
    {
        if (target != null)
        {
            StartCoroutine(ApplyBurnDamage());
        }
        Destroy(gameObject); // Destroi a bala após atingir o alvo
    }

    // Aplica o dano contínuo ao alvo
    private IEnumerator ApplyBurnDamage()
    {
        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            if (target != null)
            {
                target.GetComponent<Health>()?.TakeDamage(fireDamagePerSecond * Time.deltaTime);
            }
            elapsed += Time.deltaTime;
            yield return null; // Espera até o próximo quadro
        }
    }
}

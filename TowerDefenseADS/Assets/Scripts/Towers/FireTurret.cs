using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : Turret
{
    [SerializeField] private float fireDamagePerSecond = 5f; // Dano por segundo do fogo
    [SerializeField] private float burnDuration = 3f; // Duração do efeito de fogo

    // Método sobrescrito para disparar uma bala de fogo
    protected override void Shoot()
    {
        if (target == null) return; // Se não há alvo, não faz nada

        // Instancia a bala no ponto de disparo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletFire bulletScript = bulletObj.GetComponent<BulletFire>();

        // Configura os atributos específicos da bala de fogo
        bulletScript.SetTarget(target);
        bulletScript.SetFireDamage(fireDamagePer\Second, burnDuration);
    }
}
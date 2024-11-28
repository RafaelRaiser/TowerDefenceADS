using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torrelenta : Turret
{
    [SerializeField] private GameObject bulletSlowPrefab; // Prefab do proj�til que aplica lentid�o

    protected override void Shoot()
    {
        // Instancia o proj�til de lentid�o na posi��o do ponto de disparo
        GameObject bulletObj = Instantiate(bulletSlowPrefab, firingPoint.position, Quaternion.identity);
        Slow bulletScript = bulletObj.GetComponent<Slow>();

        // Define o alvo do proj�til
        bulletScript.SetTarget(target);
    }
}

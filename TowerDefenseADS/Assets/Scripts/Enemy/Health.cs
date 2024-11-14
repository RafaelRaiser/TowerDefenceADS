using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 

{
    [SerializeField] protected float hit = 2;    

    protected bool isDestroyed = false;    

    public virtual void Damaged(float dmg)    

    {
        hit -= dmg;// Subtrai o dano dos pontos de vida.
        if (hit <= 0 && !isDestroyed)        // Verifica se os pontos de vida chegaram a zero ou menos e se o objeto não foi destruído.

        {
            EnemySpawner.onEnemyDestroy.Invoke();// Notifica o spawner que um inimigo foi destruído.
            isDestroyed = true;// Marca o objeto como destruído.
            Destroy(gameObject);
        }
    }
}

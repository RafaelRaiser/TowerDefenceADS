using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float hit = 2; // Pontos de vida.
    protected bool isDestroyed = false; // Controle se o objeto foi destru�do.

    // M�todo para receber dano.
    public virtual void Damaged(float dmg)
    {
        hit -= dmg; // Reduz o dano dos pontos de vida.

        if (hit <= 0 && !isDestroyed) // Verifica se est� destru�do.
        {
            isDestroyed = true; // Marca como destru�do.
            GameManager.instance.AddMoney(10); // D� recompensa ao jogador.
            Destroy(gameObject); // Destroi o objeto.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float hit = 2; // Pontos de vida.
    protected bool isDestroyed = false; // Controle se o objeto foi destruído.

    // Método para receber dano.
    public virtual void Damaged(float dmg)
    {
        hit -= dmg; // Reduz o dano dos pontos de vida.

        if (hit <= 0 && !isDestroyed) // Verifica se está destruído.
        {
            isDestroyed = true; // Marca como destruído.
            GameManager.instance.AddMoney(10); // Dá recompensa ao jogador.
            Destroy(gameObject); // Destroi o objeto.
        }
    }
}

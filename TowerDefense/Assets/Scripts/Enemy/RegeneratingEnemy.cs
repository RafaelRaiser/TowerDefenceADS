using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratingEnemy : Health 
{
    [SerializeField] private float regenerationRate =2f;   
    [SerializeField] private float maxHits = 10f;   

    private void Start()     

    {
        hit = maxHits;   // Define os pontos de vida iniciais como o máximo.
        StartCoroutine(RegenerateHealth());  // Inicia a corrotina para regenerar saúde.
    }

    private IEnumerator RegenerateHealth()     // Corrotina que lida com a regeneração da saúde do inimigo.

    {
        while (!isDestroyed)         // Enquanto o inimigo não estiver destruído, continua a regenerar saúde.

        {
            if (hit< maxHits)             // Se os pontos de vida estiverem abaixo do máximo, aumenta os pontos de vida.

            {
                hit += regenerationRate * Time.deltaTime;  // Regenera saúde com base na taxa e no tempo.
                hit = Mathf.Min(hit, maxHits);   // Garante que os pontos de vida não ultrapassem o máximo.
            }
            yield return null;  // Espera o próximo quadro antes de continuar.
        }
    }

    public override void Damaged(float dmg) // Método chamado quando o inimigo recebe dano.
    {
        base.Damaged(dmg); // Chama o método Damaged da classe base.
    }
}

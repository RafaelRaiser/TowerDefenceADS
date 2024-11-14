using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratingEnemy : Health 
{
    [SerializeField] private float regenerationRate =2f;   
    [SerializeField] private float maxHits = 10f;   

    private void Start()     

    {
        hit = maxHits;   // Define os pontos de vida iniciais como o m�ximo.
        StartCoroutine(RegenerateHealth());  // Inicia a corrotina para regenerar sa�de.
    }

    private IEnumerator RegenerateHealth()     // Corrotina que lida com a regenera��o da sa�de do inimigo.

    {
        while (!isDestroyed)         // Enquanto o inimigo n�o estiver destru�do, continua a regenerar sa�de.

        {
            if (hit< maxHits)             // Se os pontos de vida estiverem abaixo do m�ximo, aumenta os pontos de vida.

            {
                hit += regenerationRate * Time.deltaTime;  // Regenera sa�de com base na taxa e no tempo.
                hit = Mathf.Min(hit, maxHits);   // Garante que os pontos de vida n�o ultrapassem o m�ximo.
            }
            yield return null;  // Espera o pr�ximo quadro antes de continuar.
        }
    }

    public override void Damaged(float dmg) // M�todo chamado quando o inimigo recebe dano.
    {
        base.Damaged(dmg); // Chama o m�todo Damaged da classe base.
    }
}

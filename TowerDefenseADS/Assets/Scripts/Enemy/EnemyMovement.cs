using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMover : MonoBehaviour

{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedMove = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;
    private void Start()
    {
        baseSpeed = speedMove; // Armazena a velocidade base.
        target = LevelManager.instance.path[pathIndex]; // Define o primeiro ponto do caminho como alvo.
    }

    // Método chamado a cada quadro para verificar a distância até o próximo ponto do caminho.
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)

        {
            pathIndex++; // Avança para o próximo ponto do caminho.

            if (pathIndex == LevelManager.instance.path.Length)

            {
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica o spawner que o inimigo foi destruído.
                Destroy(gameObject); // Destrói o objeto do inimigo.
                return;
            }
            else
            {
                target = LevelManager.instance.path[pathIndex]; // Atualiza o alvo para o próximo ponto do caminho.
            }
        }
    }
    private void FixedUpdate()     // Método chamado a cada quadro de física para mover o inimigo.

    {
        Vector2 direction = (target.position - transform.position).normalized;         // Calcula a direção do movimento em direção ao alvo e normaliza.


        rb.velocity = direction * speedMove;         // Define a velocidade do Rigidbody2D para mover o inimigo em direção ao alvo.

    }
    public void UpdateSpeed(float newSpeed) // Método para atualizar a velocidade do inimigo.
    {
        speedMove = newSpeed;
    }

    public void ResetSpeed() // Método para resetar a velocidade para o valor base.
    {
        speedMove = baseSpeed;
    }

}
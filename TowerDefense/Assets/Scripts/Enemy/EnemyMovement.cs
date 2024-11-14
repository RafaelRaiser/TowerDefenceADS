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

    // M�todo chamado a cada quadro para verificar a dist�ncia at� o pr�ximo ponto do caminho.
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)

        {
            pathIndex++; // Avan�a para o pr�ximo ponto do caminho.

            if (pathIndex == LevelManager.instance.path.Length)

            {
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica o spawner que o inimigo foi destru�do.
                Destroy(gameObject); // Destr�i o objeto do inimigo.
                return;
            }
            else
            {
                target = LevelManager.instance.path[pathIndex]; // Atualiza o alvo para o pr�ximo ponto do caminho.
            }
        }
    }
    private void FixedUpdate()     // M�todo chamado a cada quadro de f�sica para mover o inimigo.

    {
        Vector2 direction = (target.position - transform.position).normalized;         // Calcula a dire��o do movimento em dire��o ao alvo e normaliza.


        rb.velocity = direction * speedMove;         // Define a velocidade do Rigidbody2D para mover o inimigo em dire��o ao alvo.

    }
    public void UpdateSpeed(float newSpeed) // M�todo para atualizar a velocidade do inimigo.
    {
        speedMove = newSpeed;
    }

    public void ResetSpeed() // M�todo para resetar a velocidade para o valor base.
    {
        speedMove = baseSpeed;
    }

}
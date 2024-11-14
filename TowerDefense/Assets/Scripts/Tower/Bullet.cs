using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para proj�teis disparados por torres, movendo-se em dire��o ao alvo e causando dano
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D para controlar a f�sica do proj�til.
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do proj�til.
    [SerializeField] private int bulletDamage = 1; // Dano que o proj�til causa ao colidir.

    private Transform target; // Alvo atual do proj�til.

    // Define o alvo do proj�til.
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return; // Verifica se h� um alvo v�lido.

        Vector2 direction = (target.position - transform.position).normalized; // Calcula a dire��o.
        rb.velocity = direction * bulletSpeed; // Define a velocidade e dire��o do proj�til.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Aplica dano ao alvo e destr�i o proj�til ap�s a colis�o
        other.gameObject.GetComponent<Health>().Damaged(bulletDamage);
        Destroy(gameObject);
    }
}

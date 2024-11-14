using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para projéteis disparados por torres, movendo-se em direção ao alvo e causando dano
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D para controlar a física do projétil.
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do projétil.
    [SerializeField] private int bulletDamage = 1; // Dano que o projétil causa ao colidir.

    private Transform target; // Alvo atual do projétil.

    // Define o alvo do projétil.
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return; // Verifica se há um alvo válido.

        Vector2 direction = (target.position - transform.position).normalized; // Calcula a direção.
        rb.velocity = direction * bulletSpeed; // Define a velocidade e direção do projétil.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Aplica dano ao alvo e destrói o projétil após a colisão
        other.gameObject.GetComponent<Health>().Damaged(bulletDamage);
        Destroy(gameObject);
    }
}

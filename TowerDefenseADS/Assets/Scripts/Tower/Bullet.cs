using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D para controlar a física do projétil.
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do projétil.
    [SerializeField] private int bulletDamage = 1; // Dano que o projétil causa ao colidir.
    [SerializeField] public GameObject BulletPrefab;

    private Transform target; // Alvo atual do projétil.

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(BulletPrefab);
        // Aplica dano ao alvo e destrói o projétil após a colisão.
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damaged(bulletDamage);
        }
        Destroy(gameObject);
    }
}

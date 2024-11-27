using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D para controlar a f�sica do proj�til.
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do proj�til.
    [SerializeField] private int bulletDamage = 1; // Dano que o proj�til causa ao colidir.
    [SerializeField] public GameObject BulletPrefab;

    private Transform target; // Alvo atual do proj�til.

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
        // Aplica dano ao alvo e destr�i o proj�til ap�s a colis�o.
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damaged(bulletDamage);
        }
        Destroy(gameObject);
    }
}

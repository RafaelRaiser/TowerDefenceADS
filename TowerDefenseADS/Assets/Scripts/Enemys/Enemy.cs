using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamaged
{
    [SerializeField] public float moveSpeed = 100f;
    [SerializeField] private int currentWorth = 50;
    [SerializeField] protected Rigidbody2D rb;

    public Transform alvo;
    public int pathIndex = 0;
    [SerializeField] public int vidaAtual = 100;

    public virtual void Damage(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void Start()
    {
        alvo = LevelManager.main.path[pathIndex];
    }

    protected virtual void Update()
    {
        if (Vector2.Distance(alvo.position, transform.position) <= 0.1f)
        {
            Destiny();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.velocity = direcao * moveSpeed;
    }

    protected virtual void Destiny()
    {
        pathIndex++;
        if (pathIndex >= LevelManager.main.path.Length)
        {
            LevelManager.main.GameOver();
            OnDeath();
        }
        else
        {
            alvo = LevelManager.main.path[pathIndex];
        }
    }

    public virtual void OnDeath()
    {
        EnemySpawner.onEnemyDestroy?.Invoke();
        Destroy(gameObject);
        LevelManager.main.IncreaseCurrency(currentWorth);
    }
}

using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SlowEnemy : Enemy
{
    [SerializeField] private float aumentoDeVelocidade = 1.05f;

    protected override void Start()
    {
        base.Start();
        vidaAtual = 200;
    }

    protected override void Update()
    {
        base.Update();
        float distancia = Vector2.Distance(transform.position, alvo.position);

        if (distancia <= 0.2f)
        {
            Destiny();
        }
    }

    protected override void Destiny()
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
            moveSpeed *= aumentoDeVelocidade;
        }
    }

    public override void Move()
    {
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.velocity = direcao * moveSpeed;
    }

    public override void Damage(int dano)
    {
        base.Damage(dano);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        vidaAtual = 500;
    }

    public override void Damage(int dano)
    {
        base.Damage(dano);
    }

    public override void Move()
    {
        base.Move();
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
        }
    }
}

using UnityEngine;

public class Turret : MonoBehaviour, Iatacavel, IBuscadorDeAlvo
{
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected float bps = 1f;

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null || !CheckTargetIsInRange())
        {
            target = ObterAlvo();
        }

        if (target != null)
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    public Transform ObterAlvo()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            if (hit.transform.GetComponent<Health>()) return hit.transform;
        }
        return null;
    }

    protected virtual void Shoot()
    {
        if (bulletPrefab != null && firingPoint != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            bulletScript.SetTarget(target);
        }
    }

    public virtual void Atacar() { }
}


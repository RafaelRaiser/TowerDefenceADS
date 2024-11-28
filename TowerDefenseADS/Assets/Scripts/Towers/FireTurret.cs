using UnityEngine;

public class FireTurret : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireDamagePer;
    [SerializeField] private float fireRate = 1f;

    private Transform target;

    private void Update()
    {
        if (target == null) return;
        FireAtTarget();
    }

    private void FireAtTarget()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Seek(target, fireDamagePer);
        }
    }
}

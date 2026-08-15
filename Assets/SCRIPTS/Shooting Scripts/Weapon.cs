using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    [SerializeField] private string ownerTag;

    public float nextFireTime;

    public void Fire()
    {
        if (Time.time < nextFireTime || bulletPrefab == null || firePoint == null)
        {
            return;
        }
        
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet.TryGetComponent(out Projectile projectile))
        {
            projectile.ownerTag = ownerTag;
        }
    }
}

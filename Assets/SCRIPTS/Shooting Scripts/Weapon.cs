using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    [SerializeField] private string ownerTag;
    public float nextFireTime;

    [Header("Power Up Settings")]
    private bool isTripleShotActive = false;

    private Coroutine tripleShotCoroutine;
    private Coroutine attackSpeedCoroutine;

    private float originalFireRate;

    public static bool IsPowerUpPresentInScene = false;

    private void Awake()
    {
        originalFireRate = fireRate;
    }

    public void Fire()
    {
        if (Time.time < nextFireTime || bulletPrefab == null || firePoint == null)
        {
            return;
        }

        nextFireTime = Time.time + fireRate;

        if (isTripleShotActive)
        {
            FireTripleShot();
        }
        else 
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayPlayerShoot();
        }
    }

    private void FireTripleShot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Quaternion leftRotation = firePoint.rotation * Quaternion.Euler(0, 0, 15f);
        Instantiate(bulletPrefab, firePoint.position, leftRotation);

        Quaternion rightRotation = firePoint.rotation * Quaternion.Euler(0, 0, -15f);
        Instantiate(bulletPrefab, firePoint.position, rightRotation);
    }

    public void ActivateTripleShot(float duration = 8f)
    {
        IsPowerUpPresentInScene = false;

        if (tripleShotCoroutine != null)
        {
            StopCoroutine(tripleShotCoroutine);
        }

        tripleShotCoroutine = StartCoroutine(TripleShotTimerRoutine(duration));
    }

    private IEnumerator TripleShotTimerRoutine(float duration)
    {
        isTripleShotActive = true;

        yield return new WaitForSeconds(duration);

        isTripleShotActive = false;
        tripleShotCoroutine = null;
    }

    public void ActivateAttackSpeedBoost(float multiplier = 2.5f, float duration = 6f)
    {
     
        IsPowerUpPresentInScene = false;

        if (attackSpeedCoroutine != null)
        {
            StopCoroutine(attackSpeedCoroutine);
            fireRate = originalFireRate;
        }

        attackSpeedCoroutine = StartCoroutine(AttackSpeedTimerRoutine(multiplier, duration));
    }

    private IEnumerator AttackSpeedTimerRoutine(float multiplier, float duration)
    {
        fireRate = originalFireRate / multiplier;

        yield return new WaitForSeconds(duration);

        fireRate = originalFireRate;
        attackSpeedCoroutine = null;
    }
}
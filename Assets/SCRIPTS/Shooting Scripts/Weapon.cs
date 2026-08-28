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
    // Tracks whether our triple spread shot is currently active
    private bool isTripleShotActive = false;

    // Stores active timers so we can reset/extend duration if another power-up is picked up
    private Coroutine tripleShotCoroutine;
    private Coroutine attackSpeedCoroutine;

    // Base fire rate cached to restore original speed when buff expires
    private float originalFireRate;

    // Global tracking pattern so enemies know if a power-up already exists in play
    public static bool IsPowerUpPresentInScene = false;

    private void Awake()
    {
        // Cache original fire rate on start
        originalFireRate = fireRate;
    }

    public void Fire()
    {
        if (Time.time < nextFireTime || bulletPrefab == null || firePoint == null)
        {
            return;
        }

        nextFireTime = Time.time + fireRate;

        // If the upgrade is running, shoot 3 bullets spread out!
        if (isTripleShotActive)
        {
            FireTripleShot();
        }
        else // Otherwise, shoot the standard single bullet
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
        // Center Bullet (0 degrees shift)
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Left Bullet (Rotated +15 degrees counter-clockwise)
        Quaternion leftRotation = firePoint.rotation * Quaternion.Euler(0, 0, 15f);
        Instantiate(bulletPrefab, firePoint.position, leftRotation);

        // Right Bullet (Rotated -15 degrees clockwise)
        Quaternion rightRotation = firePoint.rotation * Quaternion.Euler(0, 0, -15f);
        Instantiate(bulletPrefab, firePoint.position, rightRotation);
    }

    // Called by TripleShotPowerUp.cs with duration parameter
    public void ActivateTripleShot(float duration = 8f)
    {
        // Item consumed from map
        IsPowerUpPresentInScene = false;

        // If active, stop current timer to reset duration back to max
        if (tripleShotCoroutine != null)
        {
            StopCoroutine(tripleShotCoroutine);
        }

        tripleShotCoroutine = StartCoroutine(TripleShotTimerRoutine(duration));
    }

    private IEnumerator TripleShotTimerRoutine(float duration)
    {
        isTripleShotActive = true;

        // Wait for the duration set on the power-up script
        yield return new WaitForSeconds(duration);

        isTripleShotActive = false;
        tripleShotCoroutine = null;
    }

    // Called by AttackSpeedPowerUp.cs
    public void ActivateAttackSpeedBoost(float multiplier = 2.5f, float duration = 6f)
    {
        // Item consumed from map
        IsPowerUpPresentInScene = false;

        // If active, stop current timer and reset rate before applying new boost
        if (attackSpeedCoroutine != null)
        {
            StopCoroutine(attackSpeedCoroutine);
            fireRate = originalFireRate;
        }

        attackSpeedCoroutine = StartCoroutine(AttackSpeedTimerRoutine(multiplier, duration));
    }

    private IEnumerator AttackSpeedTimerRoutine(float multiplier, float duration)
    {
        // Decrease delay between shots according to multiplier
        fireRate = originalFireRate / multiplier;

        yield return new WaitForSeconds(duration);

        // Reset back to normal speed
        fireRate = originalFireRate;
        attackSpeedCoroutine = null;
    }
}
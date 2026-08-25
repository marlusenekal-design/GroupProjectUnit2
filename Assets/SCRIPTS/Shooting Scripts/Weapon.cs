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

    // Stores active timer so we can reset/extend duration if another power-up is picked up
    private Coroutine tripleShotCoroutine;

    // Global tracking pattern so enemies know if a power-up already exists in play
    public static bool IsPowerUpPresentInScene = false;

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
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Shield Settings")]
    [SerializeField] private GameObject shieldVisual;
    public bool isShielded { get; private set; } = false;

    [Header("Visual Effects")]
    public GameObject deathParticlePrefab;

    [Header("Events")]
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onDeath;

    private Coroutine shieldCoroutine;

    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
        CheckLowHealthStatus();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        if (isShielded) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        onHealthChanged?.Invoke(currentHealth, maxHealth);
        CheckLowHealthStatus();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        onHealthChanged?.Invoke(currentHealth, maxHealth);
        CheckLowHealthStatus();
    }

    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isShielded = true;

        if (shieldVisual != null)
        {
            shieldVisual.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        isShielded = false;

        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }

        shieldCoroutine = null;
    }

    private void CheckLowHealthStatus()
    {
        if (CompareTag("Player") && ScreenFXController.Instance != null)
        {
            float healthRatio = (float)currentHealth / maxHealth;

            bool isCritical = healthRatio <= 0.25f && currentHealth > 0;
            ScreenFXController.Instance.SetLowHealthState(isCritical);
        }
    }

    private void Die()
    {
      
        if (CompareTag("Player") && ScreenFXController.Instance != null)
        {
            ScreenFXController.Instance.SetLowHealthState(false);
        }

        onDeath?.Invoke();

        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        if (LootManager.Instance != null)
        {
            LootManager.Instance.TryDropLoot(transform.position);
        }

        Destroy(gameObject);
    }
}
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    [Header("Ground Lifetime")]
    public float groundDespawnTime = 5f; // Seconds before item disappears from ground

    private bool isCollected = false;

    protected virtual void Start()
    {
        // Automatically destroy the item if left on the ground too long
        Destroy(gameObject, groundDespawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // Apply effect to player
            ApplyEffect(collision.gameObject);

            // Audio chime
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPowerUp();
            }

            // Remove item from map immediately
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(GameObject player);
}
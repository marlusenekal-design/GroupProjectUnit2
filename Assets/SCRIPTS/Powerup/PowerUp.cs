using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    [Header("Ground Lifetime")]
    public float groundDespawnTime = 5f;

    private bool isCollected = false;

    protected virtual void Start()
    {
        Destroy(gameObject, groundDespawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            ApplyEffect(collision.gameObject);

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPowerUp();
            }

            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(GameObject player);
}
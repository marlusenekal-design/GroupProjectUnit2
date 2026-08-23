using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // A flag to ensure the item isn't collected twice in the same frame
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object touching us is the Player and hasn't been picked up yet
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // Try to find the Weapon component on the player ship
            if (collision.TryGetComponent(out Weapon playerWeapon))
            {
                // Turn on triple-shot mode!
                playerWeapon.ActivateTripleShot();
            }

            // 🎵 Play your satisfying power-up collection audio chime!
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPowerUp();
            }

            // Remove the item clone from the map
            Destroy(gameObject);
        }
    }
}


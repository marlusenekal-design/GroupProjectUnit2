using UnityEngine;

public class NukePowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        // 1. Get the main camera to check screen bounds
        Camera mainCam = Camera.main;
        if (mainCam == null) return;

        // 2. Find all active enemies in the scene
        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null) continue;

            // 3. Convert enemy world position to Camera Viewport space (Values 0 to 1 mean on-screen)
            Vector3 viewportPos = mainCam.WorldToViewportPoint(enemy.transform.position);

            bool isOnScreen = viewportPos.x >= 0f && viewportPos.x <= 1f &&
                              viewportPos.y >= 0f && viewportPos.y <= 1f &&
                              viewportPos.z > 0f;

            // 4. If the enemy is visible on screen, trigger its death
            if (isOnScreen)
            {
                // If your Enemy script has a TakeDamage or Die method, call it here:
                if (enemy.TryGetComponent(out Health enemyHealth))
                {
                    enemyHealth.TakeDamage(9999); // Instant kill
                }
                else
                {
                    Destroy(enemy.gameObject);
                }
            }
        }

        if (ScreenFXController.Instance != null)
        {
            ScreenFXController.Instance.TriggerNukeImpact(0.4f, 0.5f);
        }
    }
}
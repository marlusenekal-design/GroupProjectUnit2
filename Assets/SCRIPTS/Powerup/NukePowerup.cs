using UnityEngine;

public class NukePowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject player)
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;

        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null) continue;

            Vector3 viewportPos = mainCam.WorldToViewportPoint(enemy.transform.position);

            bool isOnScreen = viewportPos.x >= 0f && viewportPos.x <= 1f &&
                              viewportPos.y >= 0f && viewportPos.y <= 1f &&
                              viewportPos.z > 0f;

            if (isOnScreen)
            {
                if (enemy.TryGetComponent(out Health enemyHealth))
                {
                    enemyHealth.TakeDamage(9999);
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
using UnityEngine;

public class EnemySpeedModifier : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speedIncreasePerLevel = 1f;

    public static float ExtraSpeedMultiplier { get; private set; } = 0f;

    private void OnEnable()
    {
        DifficultyManager.OnDifficultyIncreased += HandleDifficultyIncrease;
    }

    private void OnDisable()
    {
        DifficultyManager.OnDifficultyIncreased -= HandleDifficultyIncrease;
    }

    private void HandleDifficultyIncrease(int currentLevel)
    {
        
        ExtraSpeedMultiplier += speedIncreasePerLevel;

        Enemy[] activeEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in activeEnemies)
        {
            enemy.IncreaseSpeed(speedIncreasePerLevel);
        }
    }
}
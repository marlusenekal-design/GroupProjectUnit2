using UnityEngine;

public class SpawnerRateModifier : MonoBehaviour
{
    [Header("Spawn Acceleration")]
    [SerializeField] private float intervalReduction = 0.2f; // Seconds reduced per level
    [SerializeField] private float minimumSpawnInterval = 0.4f; // Hard floor speed limit

    private EnemySpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = GetComponent<EnemySpawnManager>();
    }

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
        if (spawnManager == null) return;

        float newInterval = Mathf.Max(minimumSpawnInterval, spawnManager.SpawnInterval - intervalReduction);
        spawnManager.SpawnInterval = newInterval;

        Debug.Log($"[SpawnerRateModifier] New spawn interval: {newInterval}s");
    }
}
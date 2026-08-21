using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 12f;
    [SerializeField] private float timer = 0f;
    public Transform spawnLocation;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector2 spawnPos = (Vector2)spawnLocation.position + Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}

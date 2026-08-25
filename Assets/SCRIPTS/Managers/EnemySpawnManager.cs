using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 12f;
    [SerializeField] private float timer = 0f;
    public Transform spawnLocation;


    private void Start()
    {
        if (spawnLocation == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                spawnLocation = player.transform;
            }
            else
            {
                Debug.LogError("There is no Player in the scene");
            }
        }
    }
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

        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs are not assigned in the EnemySpawnManager.");
            return;
        }

        GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 centerPos = spawnLocation != null ? spawnLocation.position : transform.position;

        Vector2 spawnPos = (Vector2)spawnLocation.position + Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

    public float SpawnInterval
    {
        get => spawnInterval;
        set => spawnInterval = value;
    }
}

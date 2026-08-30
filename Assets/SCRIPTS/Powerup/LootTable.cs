using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set; }

    [System.Serializable]
    public class DropItem
    {
        public string itemName;
        public GameObject itemPrefab;
        [Range(0f, 100f)] public float dropChance = 20f;
    }

    [Header("Global Settings")]
    [Range(0f, 100f)] public float overallDropChance = 30f; 

    [Header("Power-Up Table")]
    public List<DropItem> lootList = new List<DropItem>();

    private void Awake()
    {
        // Singleton Setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void TryDropLoot(Vector3 spawnPosition)
    {
        if (Random.Range(0f, 100f) > overallDropChance) return;

        float totalWeight = 0f;
        foreach (var item in lootList)
        {
            totalWeight += item.dropChance;
        }

        if (totalWeight <= 0f) return;

        float roll = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var item in lootList)
        {
            cumulative += item.dropChance;
            if (roll <= cumulative)
            {
                if (item.itemPrefab != null)
                {
                    Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                }
                break;
            }
        }
    }
}
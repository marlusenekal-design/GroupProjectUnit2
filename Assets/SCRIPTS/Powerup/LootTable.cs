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
        [Range(0f, 100f)] public float dropChance = 20f; // Weight / chance for this item
    }

    [Header("Global Settings")]
    [Range(0f, 100f)] public float overallDropChance = 30f; // Chance (0-100%) for ANY power-up to drop

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

    /// <summary>
    /// Spawns a randomized power-up at a specific world position.
    /// </summary>
    public void TryDropLoot(Vector3 spawnPosition)
    {
        // 1. Roll to see if ANY item drops
        if (Random.Range(0f, 100f) > overallDropChance) return;

        // 2. Calculate total weight of available items
        float totalWeight = 0f;
        foreach (var item in lootList)
        {
            totalWeight += item.dropChance;
        }

        if (totalWeight <= 0f) return;

        // 3. Roll for a specific item based on relative weights
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
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [System.Serializable]
    public struct EnemyScoreConfiguration
    {
        public Enemy enemyPrefab;
        public int scoreValue;
    }

    public TextMeshProUGUI scoreText;

    public List<EnemyScoreConfiguration> enemyScoreConfigurations = new List<EnemyScoreConfiguration>();

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void OnEnemyKilled(Enemy killedEnemy)
    {
        int pointsAwarded = 0;
        bool scoreFound = false;

        foreach (var config in enemyScoreConfigurations)
        {
            if (config.enemyPrefab != null && config.enemyPrefab.GetType() == killedEnemy.GetType())
            {
                pointsAwarded = config.scoreValue;
                scoreFound = true;
                break;
            }
        }

        if (!scoreFound)
        {
            pointsAwarded = 1;
            Debug.LogWarning($"No score configuration found for enemy type: {killedEnemy.GetType().Name}");
        }

        currentScore += pointsAwarded;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
    // This function lets other scripts read the current score safely
    public int GetCurrentScore()
    {
        return currentScore;
    }

}

using System;
using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    // Event for other scripts to listen to when score hits threshold
    public static event Action<int> OnDifficultyIncreased;

    [Header("Difficulty Settings")]
    [SerializeField] private int scoreInterval = 100;
    private int nextScoreThreshold;
    private int currentDifficultyLevel = 0;

    [Header("UI Feedback")]
    public GameObject speedUpPanel;
    public float notificationDuration = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        nextScoreThreshold = scoreInterval;
    }

    private void Update()
    {
        if (ScoreManager.currentScore >= nextScoreThreshold)
        {
            currentDifficultyLevel++;
            nextScoreThreshold += scoreInterval;

            if (speedUpPanel != null)
            {
                StartCoroutine(ShowNotificationRoutine());
            }

            OnDifficultyIncreased?.Invoke(currentDifficultyLevel);
        }
    }

    private IEnumerator ShowNotificationRoutine()
    {
        speedUpPanel.SetActive(true);
        yield return new WaitForSeconds(notificationDuration);
        speedUpPanel.SetActive(false);
    }
}
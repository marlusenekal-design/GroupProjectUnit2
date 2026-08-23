using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel; 
    public GameObject creditsPanel;  

    [Header("Text Displays")]
    public TextMeshProUGUI finalScoreText;

    [Header("Scene Names")]
    public string mainMenuSceneName = "MainMenu";

    // 1. Turns on the Game Over screen
    public void SetupGameOver()
    {
        gameOverPanel.SetActive(true);
        creditsPanel.SetActive(false); 

        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.GetCurrentScore();
            finalScoreText.text = "Final Score: " + finalScore;
        }
    }

    // 2. Reloads the current scene to restart the game
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 3. Takes the player to the Main Menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // 4. Opens the Credits Panel and hides the Game Over Panel
    public void GoToCredits()
    {
        gameOverPanel.SetActive(false); 
        creditsPanel.SetActive(true);   
    }

    // 5. Closes the Credits Panel and goes back to Game Over
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);  
        gameOverPanel.SetActive(true);   
    }

    // 6. Closes the game entirely
    public void QuitGame()
    {
        Debug.Log("Player quit the game!");
        Application.Quit();
    }
}

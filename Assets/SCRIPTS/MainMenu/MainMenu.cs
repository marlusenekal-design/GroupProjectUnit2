using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject instructionsPanel;

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMainMenuMusic();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay Scene");
    }

    public void ShowInstructions()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMainMenuMusic();
        }
        if (mainPanel != null)
        {
            mainPanel.SetActive(false);
        }

        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
    }

    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }

        if (mainPanel != null)
        {
            mainPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}

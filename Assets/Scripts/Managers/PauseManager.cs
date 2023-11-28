using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    void Update()
    {
        // Press "P" to pause 
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P key pressed");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            ShowPauseMenu(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            ShowPauseMenu(false);
        }
    }

    public void ShowPauseMenu(bool show)
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(show);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        ShowPauseMenu(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time scale is normal before loading a new scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Implement logic for quitting the game (can be adjusted based on your needs)
        Application.Quit();
    }

    public void ExitToHomeScreen()
    {
        // Implement logic to exit to the home screen
        SceneManager.LoadScene("MainMenu");
    }
}

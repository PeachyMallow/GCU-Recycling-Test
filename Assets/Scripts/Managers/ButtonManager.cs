using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();

        if (uiManager == null) { Debug.Log("*Ignore if you have quit to the menu* UIManager is no longer on same GO as ButtonManager"); }
    }

    public void StartOfficeLevel()
    {
        SceneManager.LoadScene("Level_1_Office");
        Time.timeScale = 1.0f;
    }

    public void StartCanteenLevel()
    {
        SceneManager.LoadScene("Level_2_Canteen");
        Time.timeScale = 1.0f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        uiManager.ShowPauseMenu(false); // beca note: combine these somehow?
        uiManager.IsPaused();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // beca note: might change in future 
        Time.timeScale = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Opens each person's respective prototyping scene
    public void AlexSelect()
    {
        SceneManager.LoadScene("AlexPrototyping");
    }

    public void BecaSelect()
    {
        SceneManager.LoadScene("BecaPrototyping");
    }

    public void JamesSelect()
    {
        SceneManager.LoadScene("JamesPrototyping");
    }

    /// <summary>
    /// Goes back to the 'Main' scene
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}

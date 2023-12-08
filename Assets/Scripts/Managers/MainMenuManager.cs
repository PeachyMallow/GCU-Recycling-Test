using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource buttonClickAudioSource;
    public AudioSource backgroundMusicAudioSource;

    private bool isAudioMuted = false;

    void Start()
    {
        // Additional initialization if needed
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Play();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        
    }

    public void HowToPlay()
    {
       
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }

    public void ToggleAudio()
    {
        isAudioMuted = !isAudioMuted;

        if (buttonClickAudioSource != null)
        {
            buttonClickAudioSource.mute = isAudioMuted;
        }

        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.mute = isAudioMuted;
        }
    }

   
}

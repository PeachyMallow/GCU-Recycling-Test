using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// essentially an audio manager now
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] 
    private AudioSource buttonClickAudioSource;

    [SerializeField] 
    private AudioSource backgroundMusicAudioSource;

    private bool isAudioMuted = false;

    void Start()
    {
        // Additional initialization if needed
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Play();
        }
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

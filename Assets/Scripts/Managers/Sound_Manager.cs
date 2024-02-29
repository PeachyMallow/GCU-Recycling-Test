using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soundmanager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;    // The icon for sound when it's on
    [SerializeField] Image soundOffIcon;   // The icon for sound when it's off
    private bool muted = false;             // Flag to track if sound is muted or not

    // Start is called before the first frame update
    void Start()
    {
        // Check if sound preference is stored, if not, set it to default (not muted)
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        // Update the button icon based on the initial sound state
        UpdateButtonIcon();

        // Apply the sound state to the AudioListener
        AudioListener.pause = muted;
    }

    // Method called when the button is pressed
    public void OnButtonPress()
    {
        // Toggle the sound state and update the AudioListener accordingly
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        // Save the updated sound state
        Save();

        // Update the button icon based on the new sound state
        UpdateButtonIcon();
    }

    // Method to update the button icon based on the sound state
    private void UpdateButtonIcon()
    {
        // Set the visibility of soundOnIcon and soundOffIcon based on the sound state
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    // Method to load the sound state from PlayerPrefs
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    // Method to save the current sound state to PlayerPrefs
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera playerCam;
    public Camera overheadCam;

    private Camera currentCam;

    /// <summary>
    /// Initialize the current camera to playerCam on start
    /// & 
    /// Disable overheadCam on start
    /// </summary>
    void Start()
    {
        currentCam = playerCam;
        overheadCam.enabled = false;
    }

    /// <summary>
    /// Switch cameras when player collides with triggerbox
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            SwitchCameras();
        }
    }

    /// <summary>
    /// Toggle camera states
    /// &
    /// Enable the new current camera
    /// </summary>
    void SwitchCameras()
    {
        currentCam.enabled = !currentCam.enabled;

        if (currentCam == playerCam)
        {
            currentCam = overheadCam;
        }
        else
        {
            currentCam = playerCam;
        }

        currentCam.enabled = true;
    }
}

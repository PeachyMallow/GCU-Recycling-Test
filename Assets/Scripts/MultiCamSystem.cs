using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamSystem : MonoBehaviour
{
    public Camera followPlayerCam;
    public Camera overheadCam;
    public Transform player;
    public float switchDistance = 10f;

    void Start()
    {
        followPlayerCam.enabled = true; // followPlayerCam is initially enabled on start
        overheadCam.enabled = false;
    }
    /// <summary>
    /// Switch to followPlayerCam when the player is beyond the switch distance 
    /// or
    /// Switch to overheadCam when the player is in the switch distance 
    /// </summary>
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Check the distance between the player and a reference point to determine camera switch

        if (distanceToPlayer < switchDistance)
        {
            followPlayerCam.enabled = false;
            overheadCam.enabled = true;
        }
        else
        {
            followPlayerCam.enabled = true;
            overheadCam.enabled = false;
        }
    }
}

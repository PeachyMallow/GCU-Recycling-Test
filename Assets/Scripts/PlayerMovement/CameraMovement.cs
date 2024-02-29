using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Reference to the player's Transform component
    /// Smoothing factor for camera movement
    /// Camera Offset 
    /// </summary>
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    /// <summary>
    /// Calculate the desired position for the camera
    /// Use Mathf.Lerp to smooth the current camera position to the desired position
    /// Update the cameras position
    /// Make the camera look at the player
    /// </summary>
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;

            transform.LookAt(player);
        }
    }
}




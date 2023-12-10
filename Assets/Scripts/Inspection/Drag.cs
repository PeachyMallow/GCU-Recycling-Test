using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 lastPos, currentPos;
    public float rotationSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
       lastPos = Input.mousePosition;
    }

    /// <summary>
    /// Rotates item on X & Y axis with mouse click & drag
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            Vector3 offset = currentPos - lastPos;
            transform.RotateAround(transform.position, Vector3.up, offset.x * rotationSpeed);
            transform.RotateAround(transform.position, Vector3.left, offset.y * rotationSpeed);
        }

        lastPos = Input.mousePosition;
    }
}

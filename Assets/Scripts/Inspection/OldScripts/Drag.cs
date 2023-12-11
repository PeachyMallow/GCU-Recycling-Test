using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 lastPos, currentPos;
    public float rotationSpeed = 0.1f;

    [SerializeField]
    private bool ableToDrag;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = Input.mousePosition;
        ableToDrag = false;
    }

    /// <summary>
    /// Rotates item on X & Y axis with mouse click & drag
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButton(0) && ableToDrag)
        {
            currentPos = Input.mousePosition;
            Vector3 offset = currentPos - lastPos;

            Transform child = transform.GetChild(0);
            transform.RotateAround(child.position, Vector3.up, offset.x * rotationSpeed);
            transform.RotateAround(child.position, Vector3.left, offset.y * rotationSpeed);
        }

        lastPos = Input.mousePosition;
    }


    public void AbleToDrag(bool a)
    {
        if (a)
        {
            ableToDrag = true;
        }

        else
        { 
            ableToDrag = false;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}

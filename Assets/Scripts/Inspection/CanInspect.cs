using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInspect : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;

    // distance item should appear from camera
    [SerializeField]
    private float offset;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        
    }

    public void Inspecting()
    {
        transform.position = mainCam.transform.position + mainCam.transform.forward * offset;
    }
}

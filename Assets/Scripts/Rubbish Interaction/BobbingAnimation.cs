using UnityEngine;
using System;
using System.Collections;

public class BobbingAnimation : MonoBehaviour
{
    float originalY;

    [SerializeField]
    [Header("Animation Settings")]
    public float floatStrength = 1; //Strength of the Sin wave
    public float freq; //Frequency of the bobbing/speed of bobbing
    public float rotationPerSecond; //Speed of rotation on Z axis
    public bool rotateY;
    public bool rotateX;
    public bool rotateZ;


    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time *freq) * floatStrength),
            transform.position.z);


        if (rotateX == true){

            transform.Rotate(new Vector3(rotationPerSecond, 0, 0) * Time.deltaTime);
        }

        if (rotateY == true)
        {
            transform.Rotate(new Vector3(0, rotationPerSecond, 0) * Time.deltaTime);
        }

        if (rotateZ == true)
        {
            transform.Rotate(new Vector3(0, 0, rotationPerSecond) * Time.deltaTime);
        }
    }
}
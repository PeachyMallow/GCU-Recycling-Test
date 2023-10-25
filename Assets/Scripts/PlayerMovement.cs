using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // add option for using WASD or Arrow input 
    [SerializeField]
    private bool wasd;

    [SerializeField]
    private bool arrows;

    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float speed;

    private float maxSpeed;


    private void Start()
    {
        maxSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            
        }
    }
}

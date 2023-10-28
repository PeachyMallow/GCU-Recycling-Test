using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // this method covers both WASD and arrow key input
    private float hInput;
    private float vInput;
    private Vector3 moveDir;


    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lerpSpeed;

    // saves what has been set in the inspector as the speed to be used
    private float maxSpeed;


    private void Start()
    {
        maxSpeed = speed;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        moveDir = transform.position + new Vector3(hInput, vInput, 0) * maxSpeed;
        transform.position = Vector3.Lerp(transform.position, moveDir, lerpSpeed);

        //moveDir = new Vector3(hInput, vInput, 0);
        //transform.Translate(moveDir * maxSpeed * Time.deltaTime);

    }
}

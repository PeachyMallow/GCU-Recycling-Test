using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    // this method covers both WASD and arrow key input
    private float hInput;
    private float vInput;
    private Vector2 moveDir;


    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float speed;

    // saves what has been set in the inspector as the speed to be used
    private float maxSpeed;

    [Header("Pick one to test different types of movement")]
    [SerializeField]
    private bool addForce = false;

    [SerializeField]
    private bool movePosition = false;

    [SerializeField]
    private bool vel = false;

    [SerializeField]
    private bool pTransform = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        maxSpeed = speed;
    }

    private void Update()
    {
        //Movement();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        moveDir = new Vector2(hInput, vInput);

        if (addForce)
        {
            Debug.Log("Using AddForce");
            rb2d.AddForce(moveDir * maxSpeed);
        }

        if (movePosition)
        {
            Debug.Log("Using MovePosition");
            rb2d.MovePosition(rb2d.position + moveDir * maxSpeed * Time.fixedDeltaTime);
        }

        if (vel)
        {
            Debug.Log("Using Velocity");
            rb2d.velocity = moveDir * maxSpeed;
        }

        if (pTransform)
        {
            Debug.Log("Using Transform.Translate");
            transform.Translate(moveDir * maxSpeed * Time.deltaTime);
        }

        // movement using transforms
        //transform.position = Vector3.Lerp(transform.position, moveDir, lerpSpeed);
    }
}

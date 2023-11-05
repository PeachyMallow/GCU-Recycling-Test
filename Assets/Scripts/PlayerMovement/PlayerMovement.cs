using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    // covers both WASD and arrow key input
    private float hInput;
    private float vInput;
    private Vector3 moveDir;

    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float maxSpeed;

    //// saves what has been set in the inspector as the speed to be used
    //private float maxSpeed;

    // these are all false, but bools in c# are automatically declared false so no need to initialize
    [Header("Pick one to test different types of movement")]
    [SerializeField]
    private bool addForce;

    [SerializeField]
    private bool movePosition;

    [SerializeField]
    private bool velocity;

    [SerializeField]
    private bool fUVelocity;

    [SerializeField]
    private bool playerTransform;

    private bool usingUpdate;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // gets the values from arrows or WASD input
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        
        //calculates where the player should move based on that input
        moveDir = new Vector3(hInput, vInput, 0.0f);

        if (usingUpdate)
        {
            Movement();
        }
    }

    private void FixedUpdate()
    {
        if (!usingUpdate)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (addForce || movePosition || playerTransform || fUVelocity)
        {
            usingUpdate = false; 

            if (addForce)
            {
                Vector3 move = (transform.up * vInput + transform.right * hInput).normalized;
                Vector3 hVel = rb.velocity;
                rb.AddForce(move * maxSpeed - hVel, ForceMode.VelocityChange);
            }

            if (movePosition)
            {
                moveDir.Normalize();
                Vector3 newPos = rb.position + moveDir * maxSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPos);
            }

            if (playerTransform)
            {
                transform.Translate(moveDir * maxSpeed * Time.fixedDeltaTime);
            }

            // using rigidbody velocity in fixed update
            if (fUVelocity)
            {
                rb.velocity = moveDir * maxSpeed;
            }
        }

         // using rigidbody velocity in update
         if (velocity)
         {
             usingUpdate = true;
             rb.velocity = moveDir * maxSpeed;
         }
    }
}

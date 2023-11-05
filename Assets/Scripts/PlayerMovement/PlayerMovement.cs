using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

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

    [SerializeField]
    private bool usingUpdate = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSpeed = speed;
    }

    private void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

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
        //if (!usingUpdate)
        //{
        //    hInput = Input.GetAxis("Horizontal");
        //    vInput = Input.GetAxis("Vertical");
        //}

        if (addForce || movePosition || pTransform)
        {
            usingUpdate = false; 

            if (addForce)
            {
                Vector3 move = (transform.up * vInput + transform.right * hInput).normalized;
                Vector3 hVel = rb.velocity;
                rb.AddForce(move * maxSpeed - hVel, ForceMode.VelocityChange);
            }

            // too fast
            if (movePosition)
            {
                moveDir.Normalize();
                Vector3 newPos = rb.position + moveDir * maxSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPos);
            }

            if (pTransform)
            {
                transform.Translate(moveDir * maxSpeed * Time.deltaTime);
            }
        }

        // using rigidbody velocity
        if (vel)
        {
            usingUpdate = true;
            //moveDir = moveDir * maxSpeed * Time.deltaTime;
            rb.velocity = moveDir * maxSpeed;
        }


        // movement using transforms
        //transform.position = Vector3.Lerp(transform.position, moveDir, lerpSpeed);
    }
}

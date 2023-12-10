using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    // covers both WASD and arrow key input
    private float hInput;
    private float vInput;
    // not a temp variable in a method as it needs a wider scope of access
    private Vector3 moveDir;

    private float targetAngle;
    private float angle;

    //[Header("How much smoothing the player rotation should have")]
    [SerializeField]
    private float turnSmoothTime;

    // revisit this and why we need ref
    //[SerializeField]
    private float turnSmoothVel;

    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float maxSpeed;

    //// saves what has been set in the inspector as the speed to be used
    //private float maxSpeed;

    // these are all false, but bools in c# are automatically declared false so no need to initialize
    
    // does not work with current rotation so removed visibility in the inspector
    private bool addForce;

    [Header("Pick one at a time to test different types of movement")]
    [SerializeField]
    private bool movePosition;

    [SerializeField]
    private bool velocity;

    [SerializeField]
    private bool fUVelocity;

    // does not work with current rotation so removed visibility in the inspector
    private bool playerTransform;

    private bool usingUpdate;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // gets the values from arrows or WASD input
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        
        // calculates the direction in which the player should move based on that input
        moveDir = new Vector3(hInput, 0.0f, vInput).normalized;

        if (usingUpdate)
        {
            Movement();
        }

        // beca note: need to revisit this to understand it better
        if (moveDir.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0.0f);
        }

        //Debug.Log("Move Direction: " + moveDir);
    }

    private void FixedUpdate()
    {
        if (!usingUpdate)
        {
            Movement();
        }

        // beca note: need to revisit this to understand it better
        if (moveDir.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0.0f);
        }
    }

    private void Movement()
    {
        if (addForce || movePosition || playerTransform || fUVelocity)
        {
            usingUpdate = false; 

            //if (addForce)
            //{
            //    Vector3 move = (transform.forward * vInput + transform.right * hInput).normalized;
            //    Vector3 hVel = rb.velocity;
            //    rb.AddForce(move * maxSpeed - hVel, ForceMode.VelocityChange);
            //}

            if (movePosition)
            {
                moveDir.Normalize();
                Vector3 newPos = rb.position + moveDir * maxSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPos);
            }

            //if (playerTransform)
            //{
            //    transform.Translate(moveDir * maxSpeed * Time.fixedDeltaTime);
            //}

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

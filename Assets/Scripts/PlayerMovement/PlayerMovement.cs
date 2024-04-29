using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveDir;

    private float targetAngle;
    private float angle;

    [Header("How much smoothing the player rotation should have")]
    [SerializeField]
    private float rotationSmoothing;

    private float turnSmoothVel;

    /// <summary>
    /// Adjusts player's top speed they can reach
    /// </summary>
    [Header("Adjusts player's speed")]
    [SerializeField]
    private float maxSpeed;

    [Header("How much drag the player will have whilst moving")]
    [SerializeField]
    private float moveDrag;

    [Header("How quickly the player will stop")]
    [SerializeField]
    private float stopDrag;

    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDir = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")), 1);

        // idle anim
        if (moveDir == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }

        // walking anim
        else
        {
            animator.SetFloat("Speed", 0.2f);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir * maxSpeed * Time.deltaTime, ForceMode.VelocityChange);

        if (moveDir.magnitude >= 0.1f) // player is moving
        {
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, rotationSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0.0f);
            rb.drag = moveDrag;
        }

        else { rb.drag = stopDrag; }  // snappier stop
    }
}

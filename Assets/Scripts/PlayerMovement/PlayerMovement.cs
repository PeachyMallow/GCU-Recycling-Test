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

    // true when player is moving
    [SerializeField] //        <-----   remove this
    private bool isMoving;

    // not a temp variable in a method as it needs a wider scope of access
    private Vector3 moveDir;

    private float targetAngle;
    private float angle;

    [Header("How much smoothing the player rotation should have")]
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

    private Animator animator;

    [SerializeField]
    private UIManager uiManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1);

        #region commentedOut
        //if (uiManager != null)
        //{
        //    if (!uiManager.InspectionActive())
        //    {
        // gets the values from arrows or WASD input
        //hInput = Input.GetAxis("Horizontal");
        //vInput = Input.GetAxis("Vertical");

        //Debug.Log("hInput: " + hInput);
        //Debug.Log("vInput: " + vInput);

        //if (Mathf.Abs(hInput) == 1 || Mathf.Abs(vInput) == 1)
        //{
        //    isMoving = true;
        //}

        //else { isMoving = false; }

        //// makes the player come to a halt quicker
        //if (isMoving)
        //{
        //    if (Mathf.Abs(hInput) < 0.5f)
        //    {
        //        hInput = 0.0f;
        //    }

        //    if (Mathf.Abs(vInput) < 0.5f)
        //    {
        //        vInput = 0.0f;
        //    }
        //}

        #endregion

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
        // movement via rb velocity
        rb.velocity = moveDir * maxSpeed;

        //movement via MovePosition
        //rb.MovePosition(transform.position + moveDir.normalized * maxSpeed * Time.deltaTime);

        // beca note: need to revisit this to understand it better
        if (moveDir.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0.0f);
        }
    }
}

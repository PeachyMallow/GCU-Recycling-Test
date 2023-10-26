using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private Vector3 moveDir;

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
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        moveDir = new Vector3(hInput, vInput, 0);
        transform.Translate(moveDir * maxSpeed * Time.deltaTime);
    }
}

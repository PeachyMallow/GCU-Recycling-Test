using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    // starting Y Rotation of Door
    [SerializeField]
    private float startYRot;

    // true when NPC or Player is in contact with a door
    [SerializeField]
    private bool doorContact;

    // how fast the door should move 
    [SerializeField]
    private float moveSpeed;

    // just to check in inspector 
    [SerializeField]
    private bool isDoorinStartRot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        //startPos = transform.rotation;
        startYRot = transform.rotation.y;
    }

    private void FixedUpdate()
    {
        Debug.Log("Mathf.Abs: " + Mathf.Abs(transform.rotation.y - startYRot));
        //Debug.Log("StartRot, CurrentYRot: " + startYRot + " : " + transform.rotation.y);

        // if the door isn't in contact with NPC or player
        if (!doorContact)
        {
            //Debug.Log("Door not in contact with anything");

            if (!IsDoorInStartRot())
            {
                ReturnToStartPos();
            }

            
            ////if currentPos is not equal to startPos 
            //if (transform.rotation.y != startYRot)
            //{
            //    //Debug.Log("door is not at starting Y position");
            //    ReturnToStartPos();
            //}
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            rb.angularDrag = 0.5f;
            doorContact = true;
        }
    }

    private void OnCollisionExit()
    {
        doorContact = false;
    }

    private bool IsDoorInStartRot()
    {
        if (Mathf.Abs(startYRot - transform.rotation.y) <= 0.001f)
        {
            isDoorinStartRot = true;
            rb.angularDrag = 100;
            return true; 
        }

        else
        {
            isDoorinStartRot = false;
            rb.angularDrag = 0.5f;
            return false; 
        }
    }

    // rotates door back to starting rotation point
    private void ReturnToStartPos()
    {
        if (transform.rotation.y < 0)
        {
            rb.AddRelativeTorque(0, moveSpeed, 0);
        }

        else
        {
            rb.AddRelativeTorque(0, -moveSpeed, 0);
        }
    }
}

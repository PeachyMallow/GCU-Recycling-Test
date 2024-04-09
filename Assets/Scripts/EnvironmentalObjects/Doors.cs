using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //[SerializeField]
    private Rigidbody rb;

    // starting Y Rotation of Door
    [SerializeField]
    private float startYRot;

    // true when NPC or Player is in contact with a door
    [SerializeField]
    private bool doorContact;

    // how fast the door should move 
    [Header("How fast the door should shut")]
    [SerializeField]
    private float moveSpeed;

    //[Header("How much weight the door should have when moving")]
    //[SerializeField]
    private float angularDragValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        startYRot = transform.rotation.y;
        angularDragValue = rb.angularDrag;
    }

    private void FixedUpdate()
    {
        // if the door isn't in contact with NPC or player
        if (!doorContact)
        {
            if (!IsDoorInStartRot())
            {
                ReturnToStartPos();
            }
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            rb.angularDrag = angularDragValue;
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
            rb.angularDrag = 100;
            return true; 
        }

        else
        {
            return false; 
        }
    }

    // rotates door back to starting rotation point
    private void ReturnToStartPos()
    {
        if (transform.rotation.y < startYRot)
        {
            rb.AddRelativeTorque(0, moveSpeed, 0);
        }

        else
        {
            rb.AddRelativeTorque(0, -moveSpeed, 0);
        }
    }
}

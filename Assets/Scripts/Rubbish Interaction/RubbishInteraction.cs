using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;
using Unity.Burst.CompilerServices;

public class RubbishInteraction : MonoBehaviour
{
    public int recycledScore; // only public until UI has been finalised

    public Animator paperBinAnimator;
    public Animator plasticBinAnimator;
    public Animator generalBinAnimator;
    public Animator foodwasteBinAnimator;

    [Header("SFX Here")]
    public AudioSource pickupSource;
    public AudioClip pickupClip;

    [Header("Assign Recycling Logo (floats above player's head) here")]
    [SerializeField]
    private GameObject depositIcon;

    [Header("Drag UIManager GameObject into here")]
    [SerializeField]
    private UIManager uiManager;

    //[SerializeField]
    //private Animator animator;

    void Start()
    {
        ResetScore();
    }

    private void Update()
    {
        RaycastHit hit;

        float raycastLength = 14f; // Adjust the ray length here 
        Vector3 raycastOrigin = transform.position + Vector3.up * 5; // Adjust the ray height here
        Vector3 raycastDirection = transform.forward; // set ray direction

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastLength))
        {
            if (hit.collider.CompareTag("Bin") && Inventory.instance.InventorySize() > 0)
            {
                depositIcon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Inventory.instance.Remove(uiManager.GetInventoryPos(), hit.transform.gameObject);
                    return;
                }
            }

            else
            {
                depositIcon.SetActive(false);
            }

            //Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);    <-- for debugging raycast
        }

        else
        {
            depositIcon.SetActive(false);
            //Debug.DrawRay(raycastOrigin, raycastDirection * raycastLength, Color.red);    <-- for debugging raycast
        }
    }

    // rubbish pickup
    private void OnTriggerEnter(Collider Rubbish)
    {
        if (Rubbish.tag == "NonRecyclable" || Rubbish.tag == "Paper" || Rubbish.tag == "LiquidInside" || Rubbish.tag == "FoodWaste" || Rubbish.tag == "Plastic")
        {
            RubbishPickup(Rubbish.gameObject);
        }
    }

    /// <summary>
    /// If player's inventory is not full, then player will pick up rubbish
    /// </summary>
    /// <param name="Rubbish"></param>
    private void RubbishPickup(GameObject Rubbish)
    {
        if (!Inventory.instance.IsInventoryFull())
        {
            Inventory.instance.Add(Rubbish.GetComponent<Pickup>().item);
            pickupSource.PlayOneShot(pickupClip);
            Rubbish.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes Score dependant on player interaction with the bin & plays Deposit anim
    /// if a is true, then increase the score & show green glow around score
    /// if a is false, then decrease the score & show red glow around score
    /// </summary>
    public void Score(bool a)
    {
        GetComponent<Animator>().Play("Deposit", -1, 0f);

        if (a)
        {
            recycledScore++;
            StartCoroutine(uiManager.ScoreDepositGlow(true));
        }

        else
        {
            recycledScore--;
            StartCoroutine(uiManager.ScoreDepositGlow(false));
        }
    }

    /// <summary>
    /// Returns player's current score
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return recycledScore;
    }

    /// <summary>
    /// Resets player's score to 1
    /// </summary>
    /// <param name="score"></param>
    public void ResetScore()
    {
        recycledScore = 1;
    }
}
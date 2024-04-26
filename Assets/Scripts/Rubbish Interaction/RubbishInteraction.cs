using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using UnityEngine.ProBuilder.MeshOperations;
//using Unity.Burst.CompilerServices;

public class RubbishInteraction : MonoBehaviour
{
    private int recycledScore;

    [Header("SFX Here")]
    public AudioSource pickupSource;
    public AudioClip pickupClip;

    [Header("Assign Recycling Logo (floats above player's head) here")]
    [SerializeField]
    private GameObject depositIcon;

    [Header("Drag UIManager GameObject into here")]
    [SerializeField]
    private UIManager uiManager;

    // for bin animation
    private GameObject currentBin;
    private Animator childBinAnim;

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
                // bin animation
                if (hit.collider.transform.gameObject.GetComponentInChildren<Animator>() != null)
                {
                    currentBin = hit.collider.transform.gameObject;

                    if (currentBin != null)
                    {
                        childBinAnim = currentBin.GetComponentInChildren<Animator>();
                        childBinAnim.SetBool("binShakingBool", true);
                    }
                }

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

    private void OnTriggerStay(Collider RubbishBin)
    {
        if (RubbishBin.tag == "Bin")
        {
            var bin = RubbishBin.GetComponent<Bins>();
            // is the bin is full?
            if (!bin.IsBinFull())
            {
                // keypress 'E' is controlled in Update()
                if (keyPressed && Inventory.instance.InventorySize() > 0 && canDeposit)
                {

                    string binType;
                    string recyclingType;

                    // Deposit rubbish into the bin
                    Inventory.instance.Remove(uiManager.GetInventoryPos(), RubbishBin.gameObject, out binType, out recyclingType);

                    bool isCorrectDeposit = (binType == recyclingType);// Check if the deposit is correct

                    if (isCorrectDeposit)
                    {
                        switch (binType)
                        {
                            case "Paper":
                                FindObjectOfType<UGS_Analytics>().Correct_Paper_Bin_Deposit(recyclingType);
                                break;
                            case "FoodWaste":
                                FindObjectOfType<UGS_Analytics>().Correct_Food_Bin_Deposit(recyclingType);
                                break;
                            case "NonRecyclable":
                                FindObjectOfType<UGS_Analytics>().Correct_General_Waste_Bin_Deposit(recyclingType);
                                break;
                            case "Plastic":
                                FindObjectOfType<UGS_Analytics>().Correct_Plastic_Bin_Deposit(recyclingType);
                                break;
                        }
                    }
                    else
                    {
                        switch (binType)
                        {
                            case "Paper":
                                FindObjectOfType<UGS_Analytics>().Incorrect_Paper_Bin_Deposit(recyclingType);
                                break;
                            case "FoodWaste":
                                FindObjectOfType<UGS_Analytics>().Incorrect_Food_Bin_Deposit(recyclingType);
                                break;
                            case "NonRecyclable":
                                FindObjectOfType<UGS_Analytics>().Incorrect_General_Waste_Bin_Deposit(recyclingType);
                                break;
                            case "Plastic":
                                FindObjectOfType<UGS_Analytics>().Incorrect_Plastic_Bin_Deposit(recyclingType);
                                break;
                        }
                    }
                }

                else if (keyPressed && numRubbish <= 0)
                {
                    Console.WriteLine("No rubbish to deposit");
                }
            }

            canDeposit = false;
        }

        if (Autopickup == false)
        {
            if (RubbishBin.tag == "NonRecyclable" || RubbishBin.tag == "Paper" || RubbishBin.tag == "LiquidInside" || RubbishBin.tag == "FoodWaste" || RubbishBin.tag == "Plastic" && Input.GetKey(KeyCode.E))
            {
                RubbishPickup(RubbishBin.gameObject);
            }
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

    /// <summary>
    /// Returns bin the player's currently at to access from Bins.cs
    /// </summary>
    /// <returns></returns>
    public GameObject CurrentBin()
    {
        if (currentBin != null)
        { 
            return currentBin; 
        }

        else { return null; }
    }
}
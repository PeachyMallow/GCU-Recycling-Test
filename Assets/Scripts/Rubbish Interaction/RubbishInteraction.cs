using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;

public class RubbishInteraction : MonoBehaviour
{
    public Text RubbishScore;
    public Text score;
    private int recycledScore;
    public int currentRecycledScore;
    private int recycledHighScore;
    private int numRubbish;
    private int numRubbishHeld;
    public float radNum = 0f;
    float currentVelocity = 0;
    public Vector3 collision = Vector3.zero;

    [Header("SFX Here")]
    [SerializeField]
    public AudioSource pickupSource;
    public AudioClip pickupClip;
    public AudioSource disposeSource;
    public AudioClip disposeClip;

    private bool isGameOver;
    public GameObject victoryMenuUI;
    public GameObject gameOverMenuUI;
    private GameObject Menu;
    public GameObject lastHit;

    [SerializeField]
    private bool Autopickup;

    [SerializeField]
    public Slider enviroMeter;

    // used to update what the player is currently holding
    [Header("Drag PlayerManager GameObject into here")]
    [SerializeField]
    private PlayerManager playerManager;

    // true when player presses E
    private bool keyPressed;

    // required to deposit one item at a time
    private bool canDeposit;

    void Start()
    {
        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        recycledScore = 5;
        recycledHighScore = recycledScore;
        enviroMeter.value = recycledScore;
        Console.WriteLine("Auto Pickup Active");
        keyPressed = false;
        canDeposit = false;
    }

    private void Update()
    {
        // when the player is depositing rubbish
        if (Input.GetKeyDown(KeyCode.E))
        {
            canDeposit = true;
            keyPressed = true;
        }
        
        //once the player has deposited a piece of rubbish
        else if (Input.GetKeyUp(KeyCode.E))
        {
            keyPressed = false;
        }

        PickupSwitch();
        EndingmenuUI();

        float currentScore = Mathf.SmoothDamp(enviroMeter.value, recycledScore, ref currentVelocity, 100 * Time.deltaTime);
        enviroMeter.value = currentScore;

        //var Ray = new Ray(this.transform.position, this.transform.forward);
        //RaycastHit hit;
        //if (Physics.Raycast(Ray, out hit, 10))
        //{
        //    lastHit = hit.transform.gameObject;
        //    collision = hit.point;
        //    Debug.Log(gameObject.name);
        //}
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        float raycastLength = 3f; // Adjust the ray length here 
        Vector3 raycastOrigin = transform.position + Vector3.up * 2; // Adjust the ray height here
        Vector3 raycastDirection = transform.forward; // set ray direction

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastLength))
        {
            if (hit.collider.CompareTag("Bin") && keyPressed)
            {
                Debug.Log(hit.collider.name);
                canDeposit = true;
            }
            else
            {
                canDeposit = false;
            }
            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(raycastOrigin, raycastDirection * raycastLength, Color.red);
            canDeposit = false;
        }
    }

    private void OnTriggerEnter(Collider Rubbish)
    {
        if (Autopickup == true)
        {
            if (Rubbish.tag == "NonRecyclable" || Rubbish.tag == "Paper" || Rubbish.tag == "LiquidInside" || Rubbish.tag == "FoodWaste" || Rubbish.tag == "Plastic")
            {
                RubbishPickup(Rubbish.gameObject);
            }
        }
        else { }

    }

    // these check if the player is in contact with the bins allowing for a deposit 
    private void OnTriggerStay(Collider RubbishBin)
    {
        if (RubbishBin.tag == "Bin")
        {
            // is the bin is full?
            if (!RubbishBin.GetComponent<Bins>().IsBinFull())
            {
                // keypress 'E' is controlled in Update()
                if (keyPressed && numRubbishHeld > 0 && canDeposit)
                {
                    // unsure if needed?
                    //recycledScore++;
                    //recycledHighScore++;
                   
                    int holding = RubbishBin.GetComponent<Bins>().DepositingLitter(numRubbishHeld);
                    numRubbishHeld = holding;
                    playerManager.UpdateInventory(holding, true, RubbishBin.gameObject);
                    Debug.Log("Score: " + recycledScore);

                    // unsure if needed?
                    /*score.text = "Rubbish Recycled : " + recycledHighScore;
                    enviroMeter.value = recycledScore;
                    RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;*/

                    disposeSource.PlayOneShot(disposeClip);
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
        if (!playerManager.InventoryFull())
        {
            numRubbish++;
            numRubbishHeld++;
            playerManager.UpdateInventory(1, false, Rubbish.gameObject);
            pickupSource.PlayOneShot(pickupClip);
            Rubbish.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes EIM Score dependant on player interaction with the bin
    /// if a is true, then increase the score
    /// if a is false, then decrease the score
    /// </summary>
    public void EIMScore(bool a)
    {
        if (a)
        {
            if (recycledScore > 0)
            {
                recycledScore++;
                enviroMeter.value = recycledScore;
            }
        }

        else
        {
            if (recycledScore > 0)
            {
                recycledScore--;
                enviroMeter.value = recycledScore;
            }
        }
    }

    public void EndingmenuUI()
    {
        if (enviroMeter.value == 10)
        {
            victoryMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (enviroMeter.value == 0)
        {
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Continue()
    {
        // Ui Buttons Functions
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        // Ui Buttons Functions
    }

    // used to Switch between manual and automatic pickup
    private void PickupSwitch()
    {
        if (Autopickup == true && Input.GetKey(KeyCode.Q))
        {
            Autopickup = false;
            Debug.Log("Key Pickup Active");
        }
        else if (Autopickup == false && Input.GetKey(KeyCode.Q))
        {
            Autopickup = true;
            Debug.Log("Auto Pickup Active");
        }
    }
}
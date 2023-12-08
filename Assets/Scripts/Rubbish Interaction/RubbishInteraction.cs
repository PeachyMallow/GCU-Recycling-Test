using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class RubbishInteraction : MonoBehaviour
{
    public Text RubbishScore;
    public Text score;
    [SerializeField]
    private int recycledScore;
    private int recycledHighScore;
    private int numRubbish;

    [SerializeField]
    private int numRubbishHeld;
    public float radNum = 0f;

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

    [SerializeField]
    private bool Autopickup;

    [SerializeField]
    public Slider enviroMeter;

    // used to update what the player is currently holding
    [Header("Drag PlayerManager GameObject into here")]
    [SerializeField]
    private PlayerManager playerManager;

    // true when player presses E
    [SerializeField]
    private bool keyPressed;

    // required to deposit one item at a time
    [SerializeField]
    private bool canDeposit;

    void Start()
    {
        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        recycledScore = 5;
        recycledHighScore = recycledScore;
        enviroMeter.value = recycledScore;
        RubbishScore.text = "Rubbish Collected : " + numRubbishHeld;
        score.text = "Rubbish Recycled : " + recycledHighScore;
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
            // checks if the bin is full
            if (!RubbishBin.GetComponent<Bins>().IsBinFull())
            {
                // keypress 'E' is controlled in Update()
                if (keyPressed && numRubbishHeld > 0 && canDeposit)
                {
                    // pass in recycled score to playermanager to increase or decrease dependant on if player recycles
                    // as of right now, the score increases regardless of bin/rubbish type 
                    //recycledScore++; // comment this out?
                    recycledHighScore++; // comment this out?
                    score.text = "Rubbish Recycled : " + recycledHighScore;

                    // actually this might be the ticket
                    enviroMeter.value = recycledScore;
                    int holding = RubbishBin.GetComponent<Bins>().DepositingLitter(numRubbishHeld);
                    numRubbishHeld = holding;
                    playerManager.UpdateInventory(holding, true, RubbishBin.gameObject);
                    RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;
                    disposeSource.PlayOneShot(disposeClip);
                    Debug.Log(recycledScore);
                }

                else if (keyPressed && numRubbish <= 0)
                {
                    Console.WriteLine("No rubbish to deposit");
                }
            }

            canDeposit = false;
        }

        //else if (RubbishBin.tag == "NonRecyclable")
        //{
        if (Autopickup == false)
        {
            if (RubbishBin.tag == "NonRecyclable" || RubbishBin.tag == "Paper" || RubbishBin.tag == "LiquidInside" || RubbishBin.tag == "FoodWaste" || RubbishBin.tag == "Plastic" && Input.GetKey(KeyCode.E))
            {
                RubbishPickup(RubbishBin.gameObject);
            }
        }
       // }
    }

    // checks if the player's inventory is full
    // if not, then the player will pick up rubbish
    private void RubbishPickup(GameObject Rubbish)
    {
        if (!playerManager.InventoryFull())
        {
            numRubbish++;
            numRubbishHeld++;
            playerManager.UpdateInventory(1, false, Rubbish.gameObject);
            pickupSource.PlayOneShot(pickupClip);
            Rubbish.gameObject.SetActive(false);
            //Destroy(Rubbish.gameObject);

            RubbishScore.text = "Rubbish Collected : " + numRubbishHeld;
        }
    }

    public void RubbishIncrease()
    {
        if (recycledScore > 0)
        {
            Debug.Log("RecycledScore before decrease: " + recycledScore);
            recycledScore--;
            enviroMeter.value = recycledScore;
            Debug.Log("RecycledScore after decrease: " + recycledScore);
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

    /// <summary>
    /// Changes EIM Score dependant on player interaction with the bin
    /// </summary>
    public void EIMScore()
    {

    }

    
    /// <summary>
    /// Current amount of rubbish the player is holding
    /// </summary>
    /// <returns></returns>
    public int CurrentAmountOfRubbish()
    {
        return numRubbishHeld;
    }
}
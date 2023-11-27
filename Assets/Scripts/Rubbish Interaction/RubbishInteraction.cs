using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RubbishInteraction : MonoBehaviour
{
    public Text RubbishScore;
    public Text score;
    private int recycledScore;
    private int recycledHighScore;
    private int numRubbish;
    [SerializeField]
    private int numRubbishHeld;

    [SerializeField]
    private bool Autopickup;

    [SerializeField]
    public Slider enviroMeter;

    // used to update what the player is currently holding
    [Header("Drag PlayerManager GameObject into here")]
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private bool keyPressed;

    [SerializeField]
    private bool atBin;

    [SerializeField]
    private bool canDeposit;

    void Start()
    {
        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        recycledScore = 0;
        recycledHighScore = recycledScore;
        enviroMeter.value = recycledScore;
        RubbishScore.text = "Rubbish Collected : " + numRubbishHeld;
        score.text = "Rubbish Recycled : " + recycledHighScore;
        Console.WriteLine("Auto Pickup Active");
        keyPressed = false;
        atBin = false;
        canDeposit = false;
    }

    private void Update()
    {
        // when the player is depositing rubbish
        if (Input.GetKeyDown(KeyCode.E))
        {
            canDeposit = true;
            keyPressed = true;
            Debug.Log("KeyPressed = true: " + keyPressed);
        }

        //once the player has deposited a piece of rubbish
        else if (Input.GetKeyUp(KeyCode.E))
        {
            keyPressed = false;
            Debug.Log("KeyPressed = false: " + keyPressed);
        }

        PickupSwitch();
    }

    private void OnTriggerEnter(Collider Rubbish)
    {
        if (Autopickup == true)
        {
            if (Rubbish.tag == "Rubbish" || Rubbish.tag == "Paper" || Rubbish.tag == "LiquidInside" || Rubbish.tag == "FoodWaste")
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
                    recycledScore++;
                    recycledHighScore++;
                    score.text = "Rubbish Recycled : " + recycledHighScore;
                    enviroMeter.value = recycledScore;
                    int holding = RubbishBin.GetComponent<Bins>().DepositingLitter(numRubbishHeld);
                    numRubbishHeld = holding;
                    playerManager.UpdateInventory(holding, true);
                    RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;
                }

                else if (keyPressed && numRubbish <= 0)
                {
                    Console.WriteLine("No rubbish to deposit");
                }
            }

            canDeposit = false;
        }

        else if (RubbishBin.tag == "Rubbish")
        {
            if (Autopickup == false)
            {
                if (RubbishBin.tag == "Rubbish" || RubbishBin.tag == "Paper" || RubbishBin.tag == "LiquidInside" || RubbishBin.tag == "FoodWaste" && Input.GetKey(KeyCode.E))
                {
                    RubbishPickup(RubbishBin.gameObject);
                }
            }
        }
    }

    // checks if the player's inventory is full
    // if not, then the player will pick up rubbish
    private void RubbishPickup(GameObject Rubbish)
    {
        if (!playerManager.InventoryFull())
        {
            numRubbish++;
            numRubbishHeld++;
            playerManager.UpdateInventory(1, false);
            Destroy(Rubbish.gameObject);
            RubbishScore.text = "Rubbish Collected : " + numRubbishHeld;
        }
    }

    public void RubbishIncrease()
    {
        if (recycledScore > 0)
        {
            recycledScore--;
            enviroMeter.value = recycledScore;
            //Debug.Log(recycledScore);
        }
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
    /// Current amount of rubbish the player is holding
    /// </summary>
    /// <returns></returns>
    public int CurrentAmountOfRubbish()
    {
        return numRubbishHeld;
    }
}

// when player is at the bin
//private void AtBin(GameObject bin)
//{
//    atBin = true;

//    if (!bin.GetComponent<Bins>().IsBinFull())
//    {
//        // keypress 'E' is controlled in Update()
//        if (Input.GetKeyDown(KeyCode.E) && numRubbishHeld > 0)
//        {
//            //keyPressed = true;
//            //Debug.Log("KeyPressed = true: " + keyPressed);
//            recycledScore++;
//            recycledHighScore++;
//            score.text = "Rubbish Recycled : " + recycledHighScore;
//            enviroMeter.value = recycledScore;
//            int holding = bin.GetComponent<Bins>().DepositingLitter(numRubbishHeld);
//            numRubbishHeld = holding;
//            playerManager.UpdateInventory(holding, true);
//            RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;

//            #region oldCode
//            //code prior to update
//            //recycledScore += numRubbishHeld;
//            //recycledHighScore += numRubbishHeld;
//            //score.text = "Rubbish Recycled : " + recycledHighScore;
//            //enviroMeter.value = recycledScore;
//            //RubbishBin.GetComponent<Bins>().DepositingLitter(numRubbishHeld);
//            //playerManager.UpdateInventory(0, true);
//            //numRubbishHeld = 0;
//            //RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;
//            //Debug.Log(recycledScore);
//            #endregion
//        }

//        else if (Input.GetKeyDown(KeyCode.E) && numRubbish <= 0)
//        {
//            Console.WriteLine("No rubbish to deposit");
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubbishInteraction : MonoBehaviour
{
    public Text trashScore;
    public Text score;
    private int recycledScore;
    private int numRubbish;
     int numRubbishHeld;

    [SerializeField]
    private bool Autopickup;

    public Slider enviroMeter;

    // used to update what the player is currently holding
    [Header("Drag PlayerManager GameObject into here")]
    [SerializeField]
    private PlayerManager playerManager;

    void Start()
    {
        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        recycledScore = 0;
        enviroMeter.value = recycledScore;
        trashScore.text = "Rubbish Collected : " + numRubbishHeld;
        score.text = "Rubbish Recycled : " + recycledScore;
        Console.WriteLine("Auto Pickup Active");
    }

    private void Update()
    {
        PickupSwitch();
    }

    private void OnTriggerEnter(Collider Rubbish)
    {
        if (Autopickup == true)
        {
            if (Rubbish.tag == "myTrash")
            {
                RubbishPickup(Rubbish.gameObject);
            }
        }
        else if (Autopickup == false)
        {
            if (Rubbish.tag == "myTrash" && Input.GetKey(KeyCode.E))
            {
                RubbishPickup(Rubbish.gameObject);
            }
        }

    }

    private void OnTriggerStay(Collider trashBin)
    {
        if (trashBin.tag == "Bin")
        {
            if (Input.GetKey(KeyCode.E) && numRubbish > 0)
            {
                recycledScore = numRubbish;
                score.text = "Trash Recycled : " + recycledScore;
                enviroMeter.value = recycledScore;
                playerManager.UpdateInventory(0, true);
                numRubbishHeld = 0;
                trashScore.text = "Trash Collected: " + numRubbishHeld;
            }
            else if (Input.GetKey(KeyCode.E) && numRubbish <= 0)
            {
                Console.WriteLine("No trash to deposit");
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
            trashScore.text = "Trash Collected : " + numRubbishHeld;
        }
    }

    private void PickupSwitch()
    {
        if (Autopickup == true && Input.GetKey(KeyCode.Q))
        {
            Autopickup = false;
        }
        else if (Autopickup == false && Input.GetKey(KeyCode.Q))
        {
            Autopickup = true;
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

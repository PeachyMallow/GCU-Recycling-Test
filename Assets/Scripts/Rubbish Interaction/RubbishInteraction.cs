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
    private int numRubbishHeld;

    [SerializeField]
    private bool Autopickup;

    public Slider enviroMeter;

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
                numRubbishHeld = 0;
                trashScore.text = "Trash Collected: " + numRubbishHeld;
            }
            else if (Input.GetKey(KeyCode.E) && numRubbish <= 0)
            {
                Console.WriteLine("No trash to deposit");
            }
        }
    }

    private void RubbishPickup(GameObject Rubbish)
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
        numRubbish++;
        numRubbishHeld++;
        Destroy(Rubbish.gameObject);
        trashScore.text = "Trash Collected : " + numRubbishHeld;
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

    public int CurrentAmountOfTrash()
    {
        return numRubbish;
    }
}

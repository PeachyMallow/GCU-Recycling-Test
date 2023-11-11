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
    private int numTrash;

    public Slider enviroMeter;

    void Start()
    {
        numTrash = 0;
        recycledScore = 0;
        enviroMeter.value = recycledScore;
        trashScore.text = "Rubbish Collected : " + numTrash;
        score.text = "Rubbish Recycled : " + recycledScore;
        Console.WriteLine("Auto Pickup Active");
    }

    private void OnTriggerEnter(Collider Trash)
    {
        if(Trash.tag == "myTrash")
        {
            numTrash++;
            Destroy(Trash.gameObject);
            trashScore.text = "Trash Collected: " + numTrash;            
        }
    }

    private void OnTriggerStay(Collider trashBin)
    {
        if (trashBin.tag == "Bin")
        {
            if (Input.GetKey(KeyCode.E) && numTrash > 0)
            {
                recycledScore = numTrash;
                score.text = "Trash Recycled : " + recycledScore;
                enviroMeter.value = recycledScore;
                numTrash = 0;
                trashScore.text = "Trash Collected: " + numTrash;
            }
            else if (Input.GetKey(KeyCode.E) && numTrash <= 0)
            {
                Console.WriteLine("No trash to deposit");
            }
        }
    }

    public int CurrentAmountOfTrash()
    {
        return numTrash;
    }
}
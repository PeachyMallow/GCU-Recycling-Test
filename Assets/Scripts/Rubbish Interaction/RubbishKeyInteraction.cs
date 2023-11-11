using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubbishKeyInteraction : MonoBehaviour
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
        Console.WriteLine("Press (E) Key to Pickup Rubbish");
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
                trashScore.text = "Trash Collected : " + numTrash;
            }
            else if (Input.GetKey(KeyCode.E) && numTrash <= 0)
            {
                Console.WriteLine("No trash to deposit");
            }
        }
        if (trashBin.tag == "myTrash" && Input.GetKey(KeyCode.E))
        {
            numTrash++;
            Destroy(trashBin.gameObject);
            trashScore.text = "Trash Collected : " + numTrash;
        }
    }
}
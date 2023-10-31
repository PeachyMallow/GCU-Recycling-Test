using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Text trashScore;
    public Text score;
    private int recycledScore;
    private int numTrash;

    void Start()
    {
        numTrash = 0;
        recycledScore = 0;
        trashScore.text = "Trash Collected : " + numTrash;
        score.text = "Trash Recycled : " + recycledScore;
    }

    private void OnTriggerEnter(Collider Trash)
    {
        if(Trash.tag == "myTrash")
        {
            numTrash++;
            Destroy(Trash.gameObject);
            trashScore.text = "Trash Collected : " + numTrash;
        }
    }

    private void OnTriggerStay(Collider trashBin)
    {
        if (trashBin.tag == "Bin")
        {
            if(Input.GetKey(KeyCode.E) && numTrash > 0)
            {
                recycledScore = numTrash;
                score.text = "Trash Recycled : " + recycledScore;
                numTrash = 0;
                trashScore.text = "Trash Collected : " + numTrash;
            }
            else if(Input.GetKey(KeyCode.E) && numTrash <= 0)
            {
                Debug.Log("No trash to deposit");
            }
        }
    }
}

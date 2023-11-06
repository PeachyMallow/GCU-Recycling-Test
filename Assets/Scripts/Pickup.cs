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

    // beca added
    public Item item;

    void Start()
    {
        numTrash = 0;
        recycledScore = 0;
        trashScore.text = "Rubbish Collected: " + numTrash;
        score.text = "Rubbish Recycled: " + recycledScore;
    }

    private void OnTriggerEnter(Collider Trash)
    {
        if(Trash.tag == "myTrash")
        {
            Hotbar.instance.Add(item);
            numTrash++;
            Destroy(Trash.gameObject);
            trashScore.text = "Rubbish Collected: " + numTrash;            
        }
    }

    private void OnTriggerStay(Collider trashBin)
    {
        if (trashBin.tag == "Bin")
        {
            if(Input.GetKey(KeyCode.E) && numTrash > 0)
            {
                recycledScore = numTrash;
                score.text = "Rubbish Recycled: " + recycledScore;
                numTrash = 0;
                trashScore.text = "Rubbish Collected: " + numTrash;
            }
            else if(Input.GetKey(KeyCode.E) && numTrash <= 0)
            {
                Debug.Log("No rubbish to deposit");
            }
        }
    }

    public int CurrentAmountOfTrash()
    {
        return numTrash;
    }
}

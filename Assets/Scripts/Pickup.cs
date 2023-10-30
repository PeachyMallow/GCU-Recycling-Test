using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Text trashScore;
    private int numTrash;

    void Start()
    {
        numTrash = 0;
        trashScore.text = "Trash Collected : " + numTrash;
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
}

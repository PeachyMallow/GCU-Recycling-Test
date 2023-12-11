using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectionObj : MonoBehaviour
{
    public GameObject[] inspectionObjects;

    //public Text objDescriptionText;
    //public Text objDescriptionText2;
    //public Text objDescriptionText3;

    private int currentIndex;

    /// <summary>
    /// // turn on item inspection canvas with relivant data for each item
    /// </summary>
    /// <param name="index"></param>
    public void TurnOnInspection(int index) 
    {
        currentIndex = index;
        //Debug.Log("Index: " + index);
        inspectionObjects[index].SetActive(true);
        //var data = inspectionObjects[index].GetComponent<InspectionObjectData>();
        //objDescriptionText.text = data.description;
        //objDescriptionText2.text=data.description1;
        //objDescriptionText3.text=data.description2;

    }

    /// <summary>
    /// turn off item inspection
    /// </summary>
    public void TurnOffInspection() 
    {
        inspectionObjects[currentIndex].SetActive(false);
    }
}

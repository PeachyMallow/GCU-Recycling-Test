using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionObj : MonoBehaviour
{
    public GameObject[] inspectionObjects;
    private int currentIndex;

    public void TurnOnInspection(int index) // turn on item inspection
    {
        currentIndex = index;
        inspectionObjects[index].SetActive(true);
    }

    public void TurnOffInspection() // turn off item inspection
    {
        inspectionObjects[currentIndex].SetActive(false);
    }
}

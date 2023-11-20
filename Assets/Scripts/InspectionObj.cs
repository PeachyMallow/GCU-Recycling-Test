using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionObj : MonoBehaviour
{
    public GameObject[] inspectionObjects;
    private int currentIndex;

    public void TurnOnInspection(int index)
    {
        currentIndex = index;
        inspectionObjects[index].SetActive(true);
    }

    public void TurnOffInspection()
    {
        inspectionObjects[currentIndex].SetActive(false);
    }
}

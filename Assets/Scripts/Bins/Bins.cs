using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bins : MonoBehaviour
{
    [Header("Amount this bin is holding")]
    [SerializeField]
    private int binCapacity;

    [SerializeField]
    private bool isBinFull;

    [Header("For Debugging")]
    [SerializeField]
    private int maxCapacity;

    private void Start()
    {
        binCapacity = 0;
        isBinFull = false;

        if (FindObjectOfType<GameManager>() != null)
        {
            maxCapacity = FindObjectOfType<GameManager>().MaxCapacity();
        }

        else { Debug.Log("Bins.cs can't find the GameManager script"); }
    }


    private void Update()
    {
        
    }

    /// <summary>
    /// Adds the deposited litter to the binsCapacity (Right now takes all the rubbish the player has in their inventory)
    /// </summary>
    public void DepositingLitter(int rubbishHeld)
    {
        
        binCapacity += rubbishHeld;
    }


    /// <summary>
    /// True if this bin is full
    /// </summary>
    /// <returns></returns>
    public bool IsBinFull()
    {
        if (binCapacity >= maxCapacity)
        {
            isBinFull = true;
            return true;
        }

        else return false;

        // return isBinFull;
    }
}

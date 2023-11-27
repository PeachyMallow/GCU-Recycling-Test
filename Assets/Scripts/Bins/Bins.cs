using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bins : MonoBehaviour
{
    [Header("All of these variables are for Debugging purposes only")]
    [Header("Amount this bin is holding")]
    [SerializeField]
    private int binCurrentlyHolding;

    [SerializeField]
    private bool isBinFull;

    [SerializeField]
    private int maxCapacity;

    private void Start()
    {
        binCurrentlyHolding = 0;
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
    public int DepositingLitter(int rubbishHeld)
    {
        if (rubbishHeld > 0)
        {
            if (!isBinFull)
            {
                binCurrentlyHolding++ /*+= rubbishHeld*/;
                rubbishHeld--;
                return rubbishHeld;
            }
        }

        else
        {
            return rubbishHeld;
        }

        return 0;
    }


    /// <summary>
    /// True if this bin is full
    /// </summary>
    /// <returns></returns>
    public bool IsBinFull()
    {
        if (binCurrentlyHolding >= maxCapacity)
        {
            isBinFull = true;
            return true;
        }

        else
        {
            return false;
        }

        // return isBinFull;
    }
}

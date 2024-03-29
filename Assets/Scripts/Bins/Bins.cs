using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bins : MonoBehaviour
{
    [Header("Drag bin full asset here")]
    [SerializeField]
    private GameObject binFull;

    [Header("Rubbish Disposal SFX")]
    [SerializeField]
    private AudioSource disposeSource;
    [SerializeField]
    private AudioClip disposeClip;

    [Header("All of these variables are for Debugging purposes only")]
    [Header("Amount this bin is holding")]
    [SerializeField]
    private int binCurrentlyHolding;

    [SerializeField]
    private bool isBinFull;

    [SerializeField]
    private int maxCapacity;

   // [SerializeField]
   // private Animator binAnimator;
   // private string binShake = "BinShake";
   // private float shakeDuration; 


    private void Start()
    {
        if (binFull != null)
        {
            binFull.SetActive(false);
        }

        else { Debug.Log("Full bin asset not assigned on " + gameObject.name); }

        binCurrentlyHolding = 0;
        isBinFull = false;

        if (FindObjectOfType<GameManager>() != null)
        {
            maxCapacity = FindObjectOfType<GameManager>().MaxCapacity();
        }

        else { Debug.Log("Bins.cs can't find the GameManager script"); }
    }

    /// <summary>
    /// Adds the deposited litter to the binsCapacity
    /// </summary>
    public void DepositingLitter()
    {
        if (!isBinFull)
        {
            binCurrentlyHolding++ /*+= rubbishHeld*/;

            //play dispose audio

            
            disposeSource.PlayOneShot(disposeClip);
           //binAnimator.Play(binShake, 0, 0.0f);

            //Inventory.instance.InventorySize();
            //return rubbishHeld;
        }
    }

        //else
        //{
        //    return rubbishHeld;
        //}

        //return 0;

    ////old code - don't delete
    ///// <summary>
    ///// Adds the deposited litter to the binsCapacity
    ///// </summary>
    //public int DepositingLitter(int rubbishHeld)
    //{
    //    if (rubbishHeld > 0)
    //    {
    //        if (!isBinFull)
    //        {
    //            binCurrentlyHolding++ /*+= rubbishHeld*/;
    //            rubbishHeld--;
    //            return rubbishHeld;
    //        }
    //    }

    //    else
    //    {
    //        return rubbishHeld;
    //    }

    //    return 0;
    //}

    /// <summary>
    /// True if this bin is full
    /// </summary>
    /// <returns></returns>
    public bool IsBinFull()
    {
        if (binCurrentlyHolding >= maxCapacity)
        {
            isBinFull = true;
            binFull.SetActive(true);
            return true;
        }

        else
        {
            return false;
        }

        // return isBinFull;
    }
}

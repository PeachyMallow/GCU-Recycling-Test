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

    private int binCurrentlyHolding;
    private bool isBinFull;
    private int maxCapacity;

    // to access RubbishInteraction
    private GameObject player;
    private RubbishInteraction rubbishInteraction;

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

        // to stop bin animation
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            rubbishInteraction = player.GetComponent<RubbishInteraction>();
        }
    }

    private void Update()
    {
        if (rubbishInteraction.CurrentBin() != null)
        {
            if (rubbishInteraction.CurrentBin() == gameObject)
            {
                if (gameObject.GetComponentInChildren<Animator>().GetBool("binShakingBool"))
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("binShakingBool", false);
                }
            }
        }
    }

    /// <summary>
    /// Adds the deposited litter to the binsCapacity
    /// </summary>
    public void DepositingLitter()
    {
        if (!isBinFull)
        {
            binCurrentlyHolding++;

            disposeSource.PlayOneShot(disposeClip);
        }
    }
}

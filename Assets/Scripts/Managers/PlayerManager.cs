using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // the amount of rubbish the player can hold
    [Header("Set the player's capacity")]
    [SerializeField]
    private int playerCapacity;

    // what he player currently has in their inventory
    private int currentlyHolding;

    [Header("Drag UI Manager GameObject here")]
    [SerializeField]
    private UIManager uiManager;

    private void Start()
    {
        uiManager.UpdateCapacityUI(currentlyHolding, playerCapacity);
    }

    // (accessed in RubbishInteraction.cs)
    /// <summary>
    /// Returns true if the player's inventory is full.  Returns false if the player's inventory is not full
    /// </summary>
    /// <returns></returns>
    public bool InventoryFull()
    {
        if (currentlyHolding >= playerCapacity)
        {
            Debug.Log("Player's inventory is full!");
            return true;
        }

        return false;
    }

    // (accessed in RubbishInteraction.cs)
    /// <summary>
    /// Updates the player's inventory upon player interaction with rubbish or the bin
    /// </summary>
    /// <param name="a"></param>
    public void UpdateInventory(int a, bool b)
    {
        if (!b)
        {
            currentlyHolding += a;
        }

        else
        {
            currentlyHolding = a;
        }

        uiManager.UpdateCapacityUI(currentlyHolding, playerCapacity);
    }
}

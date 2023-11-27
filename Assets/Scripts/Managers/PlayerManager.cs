using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // the amount of rubbish the player can hold
    [Header("Set the player's capacity")]
    [SerializeField]
    private int playerCapacity;

    // could make this equal to playerInventory.size
    // how many items the player currently has in their inventory
    [Header("All the pieces of litter the player is holding")]
    [SerializeField]
    private int currentlyHolding;

    // player's inventory
    [SerializeField]
    private List<GameObject> playerInventory = new List<GameObject>();

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
    /// a = how many items to add to player inventory count
    /// b = true when player is depositing an item, false when player picks up an item
    /// </summary>
    /// <param name="a"></param>
    public void UpdateInventory(int a, bool b, GameObject item)
    {
        // picking up litter
        if (b == false)
        {
            Debug.Log("Picked up litter");
            currentlyHolding += a;
            playerInventory.Add(item);
        }

        // depositing litter
        else
        {
            Debug.Log("Disposed of litter");
            currentlyHolding = a;

            // might need an exception here
            int index = playerInventory.IndexOf(item);

            Destroy(playerInventory[index]);
            playerInventory.RemoveAt(index);
            
        }

        uiManager.UpdateCapacityUI(currentlyHolding, playerCapacity);
    }
}

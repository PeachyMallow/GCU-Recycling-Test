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
    /// c = is either the item the player is disposing of or the bin the player is currently interacting with
    /// </summary>
    /// <param name="a"></param>
    public void UpdateInventory(int a, bool b, GameObject c)
    {
        // picking up litter
        if (b == false)
        {
            //Debug.Log("Picked up litter");
            currentlyHolding += a;
            playerInventory.Add(c);
        }

        // depositing litter
        else
        {
            //Debug.Log("Disposed of litter");
            currentlyHolding = a;

            //if (currentlyHolding >= 0)
            //{
                // might need an exception here
            //int index = playerInventory.IndexOf(c); // this is not working because I'm passing in the bloody rubbish bin game object??? 
            //Debug.Log("Index: " + index);


            string binName = c.name.Substring(0, c.name.Length - 3);
            //Debug.Log("binName: " + binName);

            // do an exists check?

            foreach (GameObject item in playerInventory)
            {
                if (item.name.StartsWith(binName))
                {
                    Debug.Log("Found matching item: " + item.name);
                    playerInventory.Remove(item);
                    break;
                }
                
                else
                {
                    Debug.Log("This item did not match: " + item.name);
                }
            }


            //if (index >= 0)
            //{
            //    Debug.Log("Item: " + c + " Index: " + index);

            //    Destroy(playerInventory[index]);
            //    playerInventory.RemoveAt(index);
            //}
            //}
        }

        uiManager.UpdateCapacityUI(currentlyHolding, playerCapacity);
    }
}

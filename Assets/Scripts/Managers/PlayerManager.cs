using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // the amount of rubbish the player can hold
    [Header("Set the player's capacity")]
    [SerializeField]
    private int playerCapacity;

    //// could make this equal to playerInventory.size
    //// how many items the player currently has in their inventory
    //[Header("All the pieces of litter the player is holding")]
    //[SerializeField]
    //private int currentlyHolding;

    // player's inventory
    [SerializeField]
    private List<GameObject> playerInventory = new List<GameObject>();

    [Header("Drag UI Manager GameObject here")]
    [SerializeField]
    private UIManager uiManager;

    // true when player's inventory has been searched for a matching piece of litter respective to what bin they are currently at
    [SerializeField]
    private bool hasSearched;

    // true when a matching piece of rubbish is found for the bin the player is currently at
    [SerializeField]
    private bool matchFound;

    private void Start()
    {
        hasSearched = false;
        matchFound = false;
        uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);
    }

    // (accessed in RubbishInteraction.cs)
    /// <summary>
    /// Returns true if the player's inventory is full.  Returns false if the player's inventory is not full
    /// </summary>
    /// <returns></returns>
    public bool InventoryFull()
    {
        if (playerInventory.Count >= playerCapacity)
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

            //currentlyHolding++;
            //currentlyHolding += a;
            playerInventory.Add(c);
        }

        // depositing litter
        else
        {
            hasSearched = false;
           //Debug.Log("hasSearched: " + hasSearched);

            //currentlyHolding = playerInventory.Count;
            //currentlyHolding = a;

            // removing 'bin' from the end of the bin currently being interacted with's name
            string binName = c.name.Substring(0, c.name.Length - 3);
            //Debug.Log("binName: " + binName);

            // do an exists check?

            if (!hasSearched)
            {
                foreach (GameObject item in playerInventory)
                {
                    //Debug.Log(item.name);

                    if (item.name.StartsWith(binName))
                    {
                        // points on EIM
                        Debug.Log("Found matching item: " + item.name);
                        playerInventory.Remove(item);
                        matchFound = true;
                        //Debug.Log("List Length: " + playerInventory.Count);
                        break;
                    }
                }

                if (!matchFound)
                {
                    // minus points on EIM
                    Debug.Log("No matching item was found");
                    playerInventory.RemoveAt(0);
                }

                matchFound = false;
                hasSearched = true;
                //Debug.Log("hasSearched: " + hasSearched);
            }
        }

        uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);
    }

    // to perform check on item being recycled


    /// <summary>
    /// Updates the players' score dependant on proper or inproper recycling
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public int UpdateScore(int score)
    {
        return score;
    }
}
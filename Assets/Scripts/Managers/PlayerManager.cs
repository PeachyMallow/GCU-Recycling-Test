using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // the amount of rubbish the player can hold
    [Header("Set the player's capacity")]
    [SerializeField]
    private int playerCapacity;

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

    [SerializeField]
    private bool hasRecycled;

    [Header("Drag RubbishInteraction script here")]
    [SerializeField]
    private RubbishInteraction rInteraction;

    [Header("Capacity UI Image Fill")]
    [SerializeField]
    public Image fill;

    private void Start()
    {
        hasSearched = false;
        matchFound = false;

        if (fill != null)
        {
            fill.fillAmount = 0;
        }
        
        else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }

        uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);

        if (rInteraction == null) 
        {
            Debug.Log("Please assign RubbishInteraction script on PlayerManager");
        }
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
        // adds it to the player's inventory list
        if (b == false)
        {
            playerInventory.Add(c);

            //added by Euan pls delete if necessary
            if (fill != null)
            {
                fill.fillAmount = (float)playerInventory.Count / playerCapacity;
            }

            else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
        }

        // depositing litter
        else
        {
            hasSearched = false;

            // removing 'bin' from the end of the bin currently being interacted with's name
            string binName = c.name.Substring(0, c.name.Length - 3);

            // if the player inventory list hasn't already been searched for a matching piece of litter corresponding
            // to the bin the player is currently interacting with
            if (!hasSearched)
            {
                foreach (GameObject item in playerInventory)
                {
                    if (item.name.StartsWith(binName))
                    {
                        // points on EIM
                        hasRecycled = true;
                        playerInventory.Remove(item);
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    // minus points on EIM
                    if (rInteraction != null)
                    {
                        rInteraction.EIMScore(false);
                    }
                    
                    playerInventory.RemoveAt(0);
                }

                hasRecycled = false;
                matchFound = false;
                hasSearched = true;
            }

            //added by Euan pls delete if necessary
            if (fill != null)
            {
                fill.fillAmount = (float)playerInventory.Count / playerCapacity;
            }

            else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
        }

        uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);
    }

    // true when player has recycled, false if player disposes of item incorrectly
    public bool Recycled()
    {
        return hasRecycled;
    }


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
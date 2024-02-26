using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    // the amount of rubbish the player can hold
    //[Header("Set the player's capacity")]
    //[SerializeField]
    //private int playerCapacity;

    //// how many items the player currently has in their inventory
    //[Header("All the pieces of litter the player is holding")]
    //[SerializeField]
    //private int currentlyHolding;

    // player's inventory
    //[SerializeField]
    //public List<GameObject> playerInventory = new List<GameObject>();

    [Header("Drag UI Manager GameObject here")]
    [SerializeField]
    private UIManager uiManager;

    // true when player's inventory has been searched for a matching piece of litter respective to what bin they are currently at
    private bool hasSearched;

    // true when a matching piece of rubbish is found for the bin the player is currently at
    //private bool matchFound;

    //[Header("Drag RubbishInteraction script here")]
    //[SerializeField]
    //private RubbishInteraction rInteraction;

    //[Header("Capacity UI Image Fill")]
    //[SerializeField]
    //public Image fill;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        hasSearched = false;
        //matchFound = false;

        //if (fill != null)
        //{
        //    fill.fillAmount = 0;
        //}
        
        //else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }

        //uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);

        //if (rInteraction == null) 
        //{
        //    Debug.Log("Please assign RubbishInteraction script on PlayerManager");
        //}
    }

    // (accessed in RubbishInteraction.cs)
    /// <summary>
    /// Returns true if the player's inventory is full.  Returns false if the player's inventory is not full
    /// </summary>
    /// <returns></returns>
    //public bool InventoryFull()
    //{
    //    if (playerInventory.Count >= playerCapacity)
    //    {
    //        Debug.Log("Player's inventory is full!");
    //        return true;
    //    }

    //    return false;
    //}

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

            Debug.Log("b was false - PlayerManager.cs");

            //playerInventory.Add(c);

            // player capacity UI
            // move to UIManager.cs? 
            //if (fill != null)
            //{
            //    fill.fillAmount = (float)playerInventory.Count / playerCapacity;
            //}

            //else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
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
                //GameObject litter = playerInventory[0];
                //Debug.Log("First item in list: " +  litter);

                //foreach (GameObject item in playerInventory)
                //{
                //    if (item.name.StartsWith(binName))
                //    {
                //        rInteraction.EIMScore(true);
                //        //playerInventory.Remove(item);
                //        //Destroy(item);
                //        //matchFound = true;
                //        break;
                //    }
                //}

                //if (!matchFound)
                //{
                //    if (rInteraction != null)
                //    {
                //        rInteraction.EIMScore(false);
                //    }
                    
                //    playerInventory.RemoveAt(0);
                //    Destroy(litter);
                //}

                //matchFound = false;
                //hasSearched = true;
            }

            // updates player capacity UI
            // move to UIManager.cs? 
            //if (fill != null)
            //{
            //    fill.fillAmount = (float)playerInventory.Count / playerCapacity;
            //}

            //else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
        }

        //uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);
    }

    //public List CurrentInventory()
    //{
    //    if (playerInventory.Count > 0)
    //    {
    //        return playerInventory;
    //    }

    //    else
    //    {
    //        return null;
    //    }
    //}
}

//// (accessed in RubbishInteraction.cs)
///// <summary>
///// Updates the player's inventory upon player interaction with rubbish or the bin
///// a = how many items to add to player inventory count
///// b = true when player is depositing an item, false when player picks up an item
///// c = is either the item the player is disposing of or the bin the player is currently interacting with
///// </summary>
///// <param name="a"></param>
//public void UpdateInventory(int a, bool b, GameObject c)
//{
//    // picking up litter
//    if (b == false)
//    {
//        playerInventory.Add(c);

//        // player capacity UI
//        // move to UIManager.cs? 
//        if (fill != null)
//        {
//            fill.fillAmount = (float)playerInventory.Count / playerCapacity;
//        }

//        else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
//    }

//    // depositing litter
//    else
//    {
//        hasSearched = false;

//        // removing 'bin' from the end of the bin currently being interacted with's name
//        string binName = c.name.Substring(0, c.name.Length - 3);

//        // if the player inventory list hasn't already been searched for a matching piece of litter corresponding
//        // to the bin the player is currently interacting with
//        if (!hasSearched)
//        {
//            GameObject litter = playerInventory[0];
//            Debug.Log("First item in list: " + litter);

//            foreach (GameObject item in playerInventory)
//            {
//                if (item.name.StartsWith(binName))
//                {
//                    rInteraction.EIMScore(true);
//                    playerInventory.Remove(item);
//                    Destroy(item);
//                    matchFound = true;
//                    break;
//                }
//            }

//            if (!matchFound)
//            {
//                if (rInteraction != null)
//                {
//                    rInteraction.EIMScore(false);
//                }

//                playerInventory.RemoveAt(0);
//                Destroy(litter);
//            }

//            matchFound = false;
//            hasSearched = true;
//        }

//        // updates player capacity UI
//        // move to UIManager.cs? 
//        if (fill != null)
//        {
//            fill.fillAmount = (float)playerInventory.Count / playerCapacity;
//        }

//        else { Debug.Log("Please assign 'PlayerCapacityFull' on PlayerManager script. UI > PlayerCapacity > PlayerCapacityFull"); }
//    }

//    uiManager.UpdateCapacityUI(playerInventory.Count, playerCapacity);
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int playerCapacity;

    [SerializeField]
    private int currentlyHolding;

    [SerializeField]
    private bool inventoryFull;

    //[Header("Add 'Player' GameObject here")]
    //[SerializeField]
    //private GameObject player;

    //[SerializeField]
    //private RubbishInteraction rInteractionScript;

    private void Start()
    {
        //rInteractionScript = player.GetComponent<RubbishInteraction>(); 
    }


    private void Update()
    {

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
    /// Updates the player's inventory
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
    }
}

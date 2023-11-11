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

    [Header("Add 'Player' GameObject here")]
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private RubbishInteraction rInteractionScript;


    private void Start()
    {
        rInteractionScript = player.GetComponent<RubbishInteraction>(); 
    }


    private void Update()
    {
        if (currentlyHolding >= playerCapacity)
        {
            inventoryFull = true;
        }
    }

    public bool InventoryFull()
    {
        return inventoryFull;
    }

    //// 
    //private void CurrentlyHolding()
    //{
    //    currentlyHolding = rInteractionScript.CurrentAmountOfTrash();
    //}

    public void UpdateInventory(int a)
    {

    }
}

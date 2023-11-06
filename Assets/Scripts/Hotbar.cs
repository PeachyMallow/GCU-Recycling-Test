using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    // total items that can be held in the hotbar
    private int totalItems = 8;

    // parent GO of the items
    [SerializeField]
    private GameObject itemsParent;

    // the current items the player has in their inventory
    [SerializeField]
    private List<GameObject> currentItemSlots = new List<GameObject>(8);

    // the icons of the items the player currently has in their inventory 
    [SerializeField]
    private List<GameObject> itemIcons = new List<GameObject>();


    private void Start()
    {
        ItemSlots();
    }


    private void Update()
    {
        
    }

    /// <summary>
    /// Returns false if the players inventory is full
    /// Returns true if the players inventory is not full
    /// </summary>
    /// <returns></returns>
    public bool IsInventoryFull()
    {
        if (currentItemSlots.Count < totalItems)
        {
            return false;
        }

        else
        {
            return true;
        }
    }


    private void ItemSlots()
    {
        // if the itemsParent has been assigned
        if (itemsParent != null)
        {
            // adds all the child game objects to the currentItems list
            foreach (Transform item in itemsParent.transform)
            {
                currentItemSlots.Add(item.gameObject);
            }

            CurrentItems();
        }

        else
        {
            Debug.Log("Hotbar items parent hasn't been assigned in GameManager");
        }
    }

    private void CurrentItems()
    {
        if (currentItemSlots != null)
        {
            foreach (GameObject item in currentItemSlots)
            {
                itemIcons.Add(item.gameObject);
            }
        }
    }
}

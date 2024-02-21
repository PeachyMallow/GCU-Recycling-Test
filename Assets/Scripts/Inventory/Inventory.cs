using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of inventory found");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public List<Item> items = new List<Item>();


    // total items that can be held in the hotbar
    public int inventorySize = 8;

    //// parent GO of the items
    //[SerializeField]
    //private GameObject itemsParent;

    //// the current items the player has in their inventory
    //[SerializeField]
    //private List<GameObject> currentItemSlots = new List<GameObject>(8); 

    //// the icons of the items the player currently has in their inventory 
    //[SerializeField]
    //private List<GameObject> itemIcons = new List<GameObject>(8);





    private void Start()
    {
        ItemSlots();
    }


    private void Update()
    {
        
    }

    public void Add(Item item)
    {
        if (!IsInventoryFull())
        {
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }        
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    // need to change this 
    /// <summary>
    /// Returns true if the players inventory is full
    /// Returns false if the players inventory is not full
    /// </summary>
    /// <returns></returns>
    public bool IsInventoryFull()
    {
        if (/*currentItemSlots*/items.Count < inventorySize)
        {
            Debug.Log("Inventory is not full");
            return false;
        }

        else
        {
            Debug.Log("Inventory is full");
            return true;
        }
    }

    // for now, hides all icon images
    private void ContainsItem()
    {
        //if (itemIcons != null)
        //{
        //    foreach (GameObject icons in itemIcons)
        //    {
        //        icons.SetActive(false);
        //    }
        //}
    }

    private void ItemSlots()
    {
        //// if the itemsParent has been assigned
        //if (itemsParent != null)
        //{
        //    // adds all the child game objects to the currentItems list
        //    foreach (Transform item in itemsParent.transform)
        //    {
        //        currentItemSlots.Add(item.gameObject);
        //    }

        //    ItemIcons();
        //}

        //else
        //{
        //    Debug.Log("Hotbar items parent hasn't been assigned in GameManager");
        //}
    }

    // accesses each item slots corresponding icon
    private void ItemIcons()
    {
        //if (currentItemSlots != null)
        //{
        //    for (int i = 0; i < currentItemSlots.Count; i++)
        //    {
        //        GameObject icon = currentItemSlots[i].transform.GetChild(0).gameObject;
        //        itemIcons.Add(icon);
        //    }

        //    ContainsItem();
        //}
    }
}

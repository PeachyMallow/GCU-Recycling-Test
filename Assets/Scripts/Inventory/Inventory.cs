using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REMOVE FUNCTION MAYBE FOR ANALYTICS

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

    [Header("Drag RubbishInteraction script here")]
    [SerializeField]
    private RubbishInteraction rInteraction;

    [Header("Drag 'InvFull' to here")]
    [SerializeField]
    private GameObject InventoryFullText;


    private void Update()
    {
        if (!IsInventoryFull())
        {
            InventoryFullText.SetActive(false);
        }
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

    // MAYBE FOR ANALYTICS HERE
    // where the rubbish & bin interaction happens 
    public void Remove(int arrayPos, GameObject bin)
    {
        // removing 'bin' from the end of the bin currently being interacted with's name
        string binNameStart = bin.name.Substring(0, bin.name.Length - 3);

        if (arrayPos <= items.Count - 1) // prevents empty slots from being selected
        {
            if (items[arrayPos].recyclingType.StartsWith(binNameStart))
            {
                rInteraction.Score(true);
            }

            else { rInteraction.Score(false); }

            items.Remove(items[arrayPos]);

            bin.GetComponent<Bins>().DepositingLitter();
        }

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
        if (items.Count < inventorySize)
        {
            return false;
        }

        else
        {
            InventoryFullText.SetActive(true);
            return true;
        }
    }

    /// <summary>
    /// Returns the current inventory size
    /// </summary>
    /// <returns></returns>
    public int InventorySize()
    {
        return items.Count;
    }
}

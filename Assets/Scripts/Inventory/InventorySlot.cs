using UnityEngine;
using UnityEngine.UI;

// handles showing/hiding icons   <---     If nothing is added, maybe change name to InventorySlotIconHandler?
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    private Item item;


    private void Start()
    {
        icon.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    //public void OnItemRemoval()
    //{
    //    Inventory.instance.Remove(item);
    //}

    public void RemoveItem()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Rubbish")]
public class Item : ScriptableObject
{
    public Sprite icon = null;
    new public string name = "Rubbish Name";
    //public string recycleType = "Unknown"; // <-- might not need
    //public GameObject item;// <-- don't think I need
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // player's capacity UI
    [Header("Drag the Capacity UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI capacityGO;

    [SerializeField]
    private GameObject inspectionUI;

    [SerializeField]
    private GameObject rightBtn;

    [SerializeField]
    private GameObject leftBtn;

    [SerializeField]
    private bool inspectionActive;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private List<GameObject> inventory = new List<GameObject>();

    //[SerializeField]
    // private CanInspect canInspect;

    // size of list
    [SerializeField]
    private int size;

    // index of current item
    [SerializeField]
    private int index;

    [SerializeField]
    private GameObject currentItem;


    private void Start()
    {
        inspectionUI.SetActive(false);
    }

    private void Update()
    {
        // shows/hides inspection UI
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!inspectionActive)
            {
                index = 0;
                inspectionUI.SetActive(true);
                inspectionActive = true;
                ItemsToInspect(); // generates list of player's current inventory

                // start on first item in inventory list

                if (inventory.Count > 0)
                {
                    CurrentlyInspecting();
                }
            }

            else
            {
                inspectionUI.SetActive(false);
                inspectionActive = false;

                if (inventory.Count > 0)
                {
                    inventory[index].SetActive(false);
                }

                // clears temp inventory list on inspection close
                if (inventory.Count > 0)
                {
                    inventory.Clear();
                }

                Time.timeScale = 1;
            }
        }
    }

    // updates the player capacity UI when the player has picked up/disposed of rubbish
    // also displays what the player's capacity limit is on screen
    public void UpdateCapacityUI(int a, int b)
    {
        if (capacityGO != null)
        {
            capacityGO.text = a + "\n_\n\n" + b;
        }
    }

    /// <summary>
    /// Adds player's current inventory to new list
    /// </summary>
    public void ItemsToInspect()
    {
        Time.timeScale = 0f;

        if (playerManager.playerInventory.Count > 0)
        {
            foreach (GameObject item in playerManager.playerInventory)
            {
                inventory.Add(item);
            }
        }
    }

    /// <summary>
    /// Current item being inspected
    /// </summary>
    public void CurrentlyInspecting()
    {
        inventory[index].GetComponent<Drag>().AbleToDrag(true);
        size = inventory.Count;
        inventory[index].SetActive(true);
        inventory[index].GetComponent<CanInspect>().Inspecting();
        currentItem = inventory[index];
    }

    //public bool InspectionActive()
    //{ return inspectionActive; }

    // inspection buttons
    // inspection UI move left button
    public void MoveLeft()
    {
        if (inventory.Count > 0)
        {
            inventory.IndexOf(currentItem);

            if (inventory.IndexOf(currentItem) > 0)
            {
                ResetDragAbility();

                index = inventory.IndexOf(currentItem) - 1;
                currentItem = inventory[index];
                CurrentlyInspecting();
            }
        }
    }

    // inspection UI move right button
    public void MoveRight()
    {
        if (inventory.Count > 0)
        {
            inventory.IndexOf(currentItem);

            if (inventory.IndexOf(currentItem) < size - 1)
            {
                ResetDragAbility();

                index = inventory.IndexOf(currentItem) + 1;
                currentItem = inventory[index];
                CurrentlyInspecting();
            }
        }
    }

    private void ResetDragAbility()
    {
        GameObject lastItem = inventory[inventory.IndexOf(currentItem)];
        lastItem.GetComponent<Drag>().AbleToDrag(false);
        lastItem.SetActive(false);
    }
}

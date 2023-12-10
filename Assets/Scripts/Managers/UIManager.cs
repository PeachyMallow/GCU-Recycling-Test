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
    private bool inspectionActive;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private List<GameObject> inventory = new List<GameObject>();

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
                inspectionUI.SetActive(true);
                inspectionActive = true;
                ItemsToInspect();
            }

            else
            {
                inspectionUI.SetActive(false);
                inspectionActive = false;

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

    public bool InspectionActive()
    { return inspectionActive; }
}

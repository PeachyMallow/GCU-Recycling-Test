using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region capacityVariablesDELETE
    //------------------------------------------------
    // will need to remove all of this probably
    //------------------------------------------------
    // player's inventory capacity UI
    [Header("Drag the Capacity UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI capacityGO;
    //------------------------------------------------
    #endregion

    // inventoryUI
    public Transform inventorySlotsParent;
    public Inventory inventory;
    public InventorySlot[] slots;
    [Header("Inventory UI SFX")]
    [SerializeField]
    private AudioSource towardsLeftSource;
    [SerializeField]
    private AudioSource towardsRightSource;
    [SerializeField]
    private AudioClip towardsLeftClip;
    [SerializeField]
    private AudioClip towardsRightClip;

    // currently selected object in inventory
    private int inventoryPos; // could change this to slots.Length?
    private int prevInventoryPos;


    #region menuScreenVariables

    // menu screens
    [Header("Drag Win UI here")]
    [SerializeField]
    public GameObject winUI;

    [Header("Drag GameOver UI here")]
    [SerializeField]
    public GameObject gameOverUI;

    [Header("Drag Pause UI here")]
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused;

    #endregion

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;
        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        inventoryPos = 0;
        prevInventoryPos = 1;
        ScrollHotbar();
    }

    private void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) // left
        {
            if (inventoryPos > 0) 
            {
                prevInventoryPos = inventoryPos;
                inventoryPos--;
                towardsLeftSource.PlayOneShot(towardsLeftClip);
            }

            if (inventoryPos == 0)
            {
                
            }

            ScrollHotbar();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket)) // right
        {
            if (inventoryPos < 7)
            {
                prevInventoryPos = inventoryPos;
                inventoryPos++;
                towardsRightSource.PlayOneShot(towardsRightClip);
            }

            ScrollHotbar();
        }
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }

            else // not sure what this is doing
            {
                slots[i].RemoveItem();
            }
        }
    }

    //working out removal
    // if item is currently highlighted (inventoryPos)
    // and player interacts with bin
    // remove that item from the hotbar

    private void ScrollHotbar()
    {
        slots[inventoryPos].transform.localScale = new Vector3(1.29f, 1.29f, 1.29f);
        
        if (prevInventoryPos >= 0)
        {
            slots[prevInventoryPos].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    // returns inventory position the player has highlighted
    public int GetInventoryPos()
    {
        return inventoryPos;
    }

    #region playerCapacityDELETE

    // updates the player capacity UI when the player has picked up/disposed of rubbish
    // also displays what the player's capacity limit is on screen
    public void UpdateCapacityUI(int a, int b)
    {
        if (capacityGO != null)
        {
            capacityGO.text = a + "\n_\n\n" + b;
        }
    }

    #endregion

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused) // pause
        {
            Time.timeScale = 0f;
            ShowPauseMenu(true);
        }

        else // resume
        {
            Time.timeScale = 1f;
            ShowPauseMenu(false);
        }
    }

    public void IsPaused()
    {
        isPaused = false;
    }

    /// <summary>
    /// Displays the pause menu dependant on bool parameter
    /// True - Display the Pause UI
    /// False - Hide the Pause UI
    /// </summary>
    /// <param name="show"></param>
    public void ShowPauseMenu(bool show)
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(show);
        }

        else { Debug.Log("Please assign PauseMenu UI on the ButtonManager"); }
    }



    /// <summary>
    /// Shows either the 'Win' or 'Lose' screen dependant on the bool parameter
    /// result = true: win, result = false: lose
    /// </summary>
    /// <param name="result"></param>
    public void WinOrLose(bool result)
    {
        if (result)
        {
            if (winUI != null)
            {
                winUI.SetActive(true);
                Time.timeScale = 0.0f;
            }

            else { Debug.Log("Victory screen has not been assigned in UIManager Inspector"); }
        }

        else
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
                Time.timeScale = 0.0f;
            }

            else { Debug.Log("GameOver screen has not been assigned in UIManager Inspector"); }
        }
    }
}

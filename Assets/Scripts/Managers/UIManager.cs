using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region capacityVariablesDELETE
    //------------------------------------------------
    // will need to remove all of this probably
    //------------------------------------------------
    // player's inventory capacity UI
    //[Header("Drag the Capacity UI GameObject here")]
    //[SerializeField]
    //private TextMeshProUGUI capacityGO;
    //------------------------------------------------
    #endregion

    #region inventoryVariables
    [Header("Inventory\n")]

    // inventoryUI
    [Header("Drag Hotbar's Child GO, 'Items' Here")]
    public Transform inventorySlotsParent;
    [Header("Drag PlayerManager Here")]
    public Inventory inventory;
    [Header("!!Doesn't need anything assigned!!\nIn play, will show player's inventory")]
    public InventorySlot[] slots;

    // inventory SFX
    [Header("Inventory SFX")]
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
    #endregion

    #region timerVariables
    [Header("\n----------------------------\n\n\nTimer\n")]
    
    // timer UI
    [Header("Drag the TimerTxt UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI timerGO;

    // holds the GameManager script to access its methods
    [Header("Drag the GameManager GameObject here")]
    [SerializeField]
    private GameManager gM;

    // separating the time into seconds and minutes to use for UI purposes
    private float mins;
    private float secs;
    #endregion

    #region menuScreenVariables

    [Header("\n----------------------------\n\n\nMenu Screens\n")]
    // menu screens
    [Header("Win Screen")]
    [SerializeField]
    public GameObject winUI;
    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;
    public Sprite starFull;
    public Sprite starEmpty;

    [Header("Enter Value as ORIGINAL - NOT x25")]
    [SerializeField]
    public float starOneThresh;
    public float starTwoThresh;
    public float starThreeThresh;

    [Header("Drag GameOver UI here")]
    [SerializeField]
    public GameObject gameOverUI;

    [Header("Drag Pause UI here")]
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused;

    #endregion

    #region RubbishInteraction/Score System (Euan's Feel free to delete if replaced)
    public RubbishInteraction rubbishInteraction;
    [SerializeField]
    public TextMeshProUGUI displayScoreGO;
    public int displayScoreVar;
    public string displayScoreText;
    public TextMeshProUGUI displayFinalScore;
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
        // pause screen
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }

        // scrolling hotbar
        if (Input.GetKeyDown(KeyCode.LeftBracket)) // left
        {
            if (inventoryPos > 0) 
            {
                prevInventoryPos = inventoryPos;
                inventoryPos--;
                towardsLeftSource.PlayOneShot(towardsLeftClip);
            }

            if (inventoryPos == 0) // what is this doing?
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

        // timer
        if (timerGO != null)
        {
            DisplayTime();
        }

        else { Debug.Log("Please attach the timer UI to the UI Manager in the Inspector (in the hierarchy UI > Timer > TimerTxt)"); }

        //Score??
        displayScoreVar = (rubbishInteraction.recycledScore * 25) - 25;
        displayScoreText = displayScoreVar.ToString();
        displayScoreGO.text = "Score: " + displayScoreText;
        displayFinalScore.text = "Final Score: " + displayScoreText;
    }

    #region inventoryMethods
    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }

            else // not sure what this is doing - might be removing the item visually?
            {
                slots[i].RemoveItem();
            }
        }
    }

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
    #endregion

    //#region playerCapacityDELETE

    //// updates the player capacity UI when the player has picked up/disposed of rubbish
    //// also displays what the player's capacity limit is on screen
    //public void UpdateCapacityUI(int a, int b)
    //{
    //    if (capacityGO != null)
    //    {
    //        capacityGO.text = a + "\n_\n\n" + b;
    //    }
    //}

    //#endregion

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

    // -------------------------------------------------
    // might not need this anymore with game loop change
    // will use this for timer = GameOver in the meantime
    // -------------------------------------------------
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

    // converts float into minutes and seconds to display correctly
    private void DisplayTime()
    {
        mins = Mathf.FloorToInt(gM.CurrentTime() / 60);
        secs = Mathf.FloorToInt(gM.CurrentTime() % 60);
        timerGO.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}

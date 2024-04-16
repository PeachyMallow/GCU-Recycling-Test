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
    [Header("\n----------------------------\n\n\nTimer Handle UI - Truck\n")]

    [Header("Drag Handle (Truck) GameObject here")]
    [SerializeField]
    private GameObject truckGO;

    private Vector3 truckStartScale;
    private Vector3 truckEndScale;

    //[SerializeField]
    //private float overallTimeElapsed;

    //[SerializeField]
    // how long the truck has been scaling up or scaling down
    private float scaleTimeElapsed;

    [Header("How long the truck animation should be overall")]
    [SerializeField]
    private float lerpDuration;

    [Header("How long the truck takes to reach max/min size")]
    [SerializeField]
    private float scaleDuration;

    // calculated in Start() dependant on what is set in the inspector for lerpDuration & scaleDuration
    private int numTimesToScale;

    // how many times the truck has scaled up then down
    private int scaleCounter;

    // holds the GameManager script to access its methods
    [Header("Drag the GameManager GameObject here")]
    [SerializeField]
    private GameManager gM;

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
        // inventory
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateInventoryUI;
        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        inventoryPos = 0;
        prevInventoryPos = 1;
        ScrollHotbar();

        // timer truck handle
        truckStartScale = truckGO.transform.localScale;
        truckEndScale = truckStartScale * 1.132f;
        numTimesToScale = Mathf.FloorToInt((lerpDuration / scaleDuration) / 2);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)) //                                                         <-------- TEMP
        //{
        //    if (overallTimeElapsed < lerpDuration)
        //    {
        //        StartCoroutine(ThresholdAnim());
        //    }
        //}

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

    /// <summary>
    /// Scales the Truck slider timer image by the numTimesToScale int calculated in Start()
    /// </summary>
    /// <returns></returns>
    public IEnumerator ThresholdAnim()
    {
        while (scaleCounter < numTimesToScale)
        { 
            scaleTimeElapsed = 0;

            while (scaleTimeElapsed < scaleDuration)
            {
                truckGO.transform.localScale = Vector3.Lerp(truckStartScale, truckEndScale, scaleTimeElapsed / scaleDuration);
                scaleTimeElapsed += Time.deltaTime;
                //overallTimeElapsed += Time.deltaTime;

                yield return null;
            }

            scaleTimeElapsed = 0;

            while (scaleTimeElapsed < scaleDuration)
            {
                truckGO.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, scaleTimeElapsed / scaleDuration);
                scaleTimeElapsed += Time.deltaTime;
                //overallTimeElapsed += Time.deltaTime;

                yield return null;
            }

            scaleCounter++;
        }

        scaleCounter = 0;
    }
}

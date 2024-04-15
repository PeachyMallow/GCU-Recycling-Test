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

    [Header("Truck Handler (Image)")]
    [Header("Drag Handle (Truck) GameObject here")]
    [SerializeField]
    private GameObject truckGO;

    [SerializeField]
    private Vector3 truckStartScale;

    [SerializeField]
    private Vector3 truckEndScale;

    //[SerializeField]
    //private bool truckShrinking;

    // from tut
    [SerializeField]
    private float overallTimeElapsed;

    [SerializeField]
    private float scaleTimeElapsed;

    [SerializeField]
    private float lerpDuration;

    [SerializeField]
    private float scaleDuration;

    [SerializeField]
    private int numTimesToScale;

    [SerializeField]
    private int scaleCounter;

    [SerializeField]
    private bool tTimer;

    [SerializeField]
    private float scaleTimer;

    [SerializeField]
    private bool timerAnimActive;



    //[SerializeField]
    //private float startValue = 0;

    //[SerializeField]
    //float endValue = 10;

    //[SerializeField]
    //float valueToLerp;


    //// timer UI
    //[Header("Drag the TimerTxt UI GameObject here")]
    //[SerializeField]
    //private TextMeshProUGUI timerGO;

    // holds the GameManager script to access its methods
    [Header("Drag the GameManager GameObject here")]
    [SerializeField]
    private GameManager gM;

    //// separating the time into seconds and minutes to use for UI purposes
    //private float mins;
    //private float secs;
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
        truckEndScale = truckStartScale * 1.132f; // put this in lerp instead of endscale?
        numTimesToScale = Mathf.FloorToInt((lerpDuration / scaleDuration) / 2);
        //scaleDuration = lerpDuration;
        //scaleTimer = 
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T)) //                                                         <-------- TEMP
        {
            if (overallTimeElapsed < lerpDuration)
            {
                StartCoroutine(ThresholdAnim());
                //truckAnimActive = true;
                //Timer(overallTimeElapsed);
                //TruckTimeThresholdAnim();
                //StartCoroutine(ThresholdAnim());
            }

            else
            {
                //truckAnimActive = false;
                 
            }
        }

        //if (truckAnimActive)
        //{
        //    Timer(overallTimeElapsed);
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

        //// timer
        //if (timerGO != null)
        //{
        //    DisplayTime();
        //}

        //else { Debug.Log("Please attach the timer UI to the UI Manager in the Inspector (in the hierarchy UI > Timer > TimerTxt)"); }

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

    // change to coroutine
    public void TruckTimeThresholdAnim(/*float time, bool a*/)
    {
        for (int i = 0; i < numTimesToScale; i++)
        {
            //Debug.Log("I: " + i);
            //if (!timerAnimActive)
            //{
                Debug.Log("I in if: " + i);
                StartCoroutine(ThresholdAnim());
            //}
        }

        //if (overallTimeElapsed < lerpDuration)
        //{
        //    return;
        //}
            //Debug.Log("Pulse called"); // checking amount of calls
            //if (a)
            //{
            // calculation first for size to scale up to? 
            //truck.transform.localScale = Vector3.Lerp(truckStartScale, truckEndScale, 0.2f);
            // sets the 'truckShrinking' bool dependant on the truck's current scale
            // true when the truck's scale is at it's largest
            // false when the truck's scale is at it's smallest
            //if (truck.transform.localScale == truckEndScale) { truckShrinking = true; }
            //else { truckShrinking = false; }
            //overallTimeElapsed = 0; // BREAKS GAME


            //numTimesToScale & scaleCounter

            //scaleTimeElapsed = 0;
            //truckShrinking = false;

            //while (overallTimeElapsed < lerpDuration)
            //{
            //    if (scaleCounter >= numTimesToScale)
            //    {
            //        return;
            //    }

            //StartCoroutine(TruckGrow());
            //StartCoroutine(TruckShrink());

            //while (scaleTimeElapsed < scaleDuration)
            //{
            //    truckGO.transform.localScale = Vector3.Lerp(truckStartScale, truckEndScale, scaleTimeElapsed / scaleDuration);
            //    scaleTimeElapsed += Time.deltaTime;
            //    overallTimeElapsed += Time.deltaTime;
            //    yield return null;
            //yield return new WaitForSeconds(scaleDuration);

            //truckGO.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, scaleTimeElapsed / scaleDuration);
            //scaleTimeElapsed += Time.deltaTime;
            //overallTimeElapsed += Time.deltaTime;
            ////yield return null;
            //scaleCounter++;
            //yield return new WaitForSeconds(scaleDuration);
            //}


            //}

            // return;

            // yield return new WaitForSeconds(scaleDuration);

            //StartCoroutine(TruckShrink());

            //truckShrinking = true;
            //}

            #region forRef
            //timeElapsed = 5;
            //truck.transform.localScale = truckEndScale;
            //if (Mathf.Abs(truck.transform.localScale.x - truckEndScale.x) <= 0.001f)
            //   {
            //  Debug.Log("Truck is fully scaled up");
            //  truck.transform.localScale = truckEndScale;
            //  truckShrinking = true;
            //timeElapsed = 0;
            //  }
            //}

            // else
            //{
            //truck.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, timeElapsed / lerpDuration);

            //if (Mathf.Abs(truck.transform.localScale.x - truckStartScale.x) <= 0.001f)
            //{
            //    Debug.Log("Truck is fully scaled down");
            //    truck.transform.localScale = truckStartScale;
            //    truckShrinking = false;
            //}
            //}

            //timeElapsed += Time.deltaTime;
            //}

            //else
            //{
            //    truck.transform.localScale = truckEndScale;
            //    //lerpDuration = timeElapsed + 0.1f; // maybe not needed
            //    truckShrinking = true;
            //    timeElapsed = 0.0f;
            //}

            //if (truck.transform.localScale == truckEndScale)
            // {
            //   truck.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, timeElapsed / lerpDuration);
            //}
            #endregion
        }
    public IEnumerator ThresholdAnim()
    {
        //scaleTimeElapsed = 0;
        //overallTimeElapsed = 0;

        //while (overallTimeElapsed < lerpDuration)
        // {

        //overallTimeElapsed += Time.deltaTime;

        //if (Mathf.Abs(truckGO.transform.localScale.x - truckEndScale.x) <= 0.001f)
        //{
        // truckGO.transform.localScale = Vector3.Lerp(truckStartScale, truckEndScale, scaleTimeElapsed / scaleDuration);
        // scaleTimeElapsed += Time.deltaTime;

        // yield return null;
        //}

        //timerAnimActive = true;
        while (scaleCounter < numTimesToScale)
        { 
            scaleTimeElapsed = 0;

            while (scaleTimeElapsed < scaleDuration)
            {
                truckGO.transform.localScale = Vector3.Lerp(truckStartScale, truckEndScale, scaleTimeElapsed / scaleDuration);
                scaleTimeElapsed += Time.deltaTime;
                overallTimeElapsed += Time.deltaTime;

                yield return null;
            }

            scaleTimeElapsed = 0;

            while (scaleTimeElapsed < scaleDuration)
            {
                truckGO.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, scaleTimeElapsed / scaleDuration);
                scaleTimeElapsed += Time.deltaTime;
                overallTimeElapsed += Time.deltaTime;

                yield return null;
            }

            scaleCounter++;
        }

        scaleCounter = 0;
        //timerAnimActive = false;
        // }

        //StartCoroutine(TruckShrink());
    }

    private IEnumerator TruckShrink()
    {
        scaleTimeElapsed = 0;
        //overallTimeElapsed = 0;

        while (overallTimeElapsed < lerpDuration)
        {
           // while (scaleTimeElapsed < scaleDuration)
        //{
            truckGO.transform.localScale = Vector3.Lerp(truckEndScale, truckStartScale, scaleTimeElapsed / scaleDuration);
           // scaleTimeElapsed += Time.deltaTime;
            //overallTimeElapsed += Time.deltaTime;
            yield return null;
        }
        
        //StartCoroutine(TruckThresholdAnim());
    }
    private float Timer(float timer)
    {
        //Debug.Log("Timer called");
        return timer -= Time.deltaTime;
    }


    // converts time type from float to int
    //private void TimeToInt()
    //{
    //    timeAsInt = Mathf.FloorToInt(gM.CurrentTime());
    //}

    //// converts float into minutes and seconds to display correctly
    //private void DisplayTime()
    //{
    //    mins = Mathf.FloorToInt(gM.CurrentTime() / 60);
    //    secs = Mathf.FloorToInt(gM.CurrentTime() % 60);
    //    timerGO.text = string.Format("{0:00}:{1:00}", mins, secs);
    //}
}

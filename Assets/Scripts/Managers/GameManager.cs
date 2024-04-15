using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
using TMPro;
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region itemSpawnVariablesDELETEATSOMEPOINT
    //[Header("Litter Spawn\n")]

    // litter's parent
    //[Header("Drag Litter Parent GameObject here")]
    //[SerializeField]
    //private Transform litterParent;

    // item spawn
    //[Header("Drag litter prefabs here\nOnly items added here will spawn")]
    //[SerializeField]
    //private GameObject[] litter;

    // time before item spawn should begin
    //[Header("Time Before Item Spawn Should Begin")]
    //[SerializeField]
    //private float startDelay;

    // true once the start delay has been called
    //private bool delayOccurred;

    // time between items spawning
    //[Header("Time in seconds for litter spawning")]
    //[SerializeField]
    //private float litterSpawnTime;

    // y position of litter to be instantiated
    //[Header("Point on Y axis litter should spawn")]
    //[SerializeField]
    //private float litterPosY;

    // beca note: could change these to be some type of co-ordinate?
    //[Header("The Area Where the Litter Should Spawn")]
    //[Header("X Min")]
    //[SerializeField]
    //private int xMin;

    //[Header("X Max")]
    //[SerializeField]
    //private int xMax;

    //[Header("Z Min")]
    //[SerializeField]
    //private int zMin;

    //[Header("Z Max")]
    //[SerializeField]
    //private int zMax;

    // temp variables
    //[Header("For Debugging")]
    //// position of litter to be instantiated
    //[Header("Position for litter to spawn")]
    //[SerializeField]
    //private Vector3 litterPos;

    // true if there is an item has been spawned
    //private bool readyToSpawn;
    #endregion

    [Header("Drag RubbishInteraction script here\n(currently on Player)")]
    [SerializeField]
    private RubbishInteraction RI;


    #region Starting Countdown
    [Header("Drag Countdown TextMeshPro here")]
    [SerializeField]
    public TextMeshProUGUI countdownText;
    [SerializeField]
    private AudioSource threeSfxSource;
    [SerializeField]
    private AudioSource twoSfxSource;
    [SerializeField]
    private AudioSource oneSfxSource;
    [SerializeField]
    private AudioSource goSfxSource;
    [SerializeField]
    private AudioClip threeSFX;
    [SerializeField]
    private AudioClip twoSFX;
    [SerializeField]
    private AudioClip oneSFX;
    [SerializeField]
    private AudioClip goSFX;

    #endregion

    [Header("\n----------------------------\n\n\nBin Capacity\n")]

    [Header("Max Capacity of each bin")]
    [SerializeField]
    private int binsMaxCapacity;

    [Header("\n----------------------------\n\n\nTimer\n")]
    
    #region timerVariables
    // timer set in inspector
    [Header("Enter time in seconds")]
    [SerializeField]
    private float timer;

    [SerializeField]
    private int timeAsInt;

    [Header("Truck Handler (Audio)")]

    // three bool below are used to make the 'truck' audio play once
    private bool thresh1;
    private bool thresh2;
    private bool thresh3;

    [SerializeField]
    private AudioSource truckSource;

    [SerializeField]
    private AudioClip truckClip;

    [Header("Drag UIManager GameObject Here")]
    [SerializeField]
    private UIManager uIManager;

    // saves the time alotted in the inspector to reset the timer
    private float totalTime;

    [SerializeField]
    private int halfTime;

    // is the timer active
    [SerializeField]
    private bool timerActive;

    //Audio Management with Timer
    [Header("Drag Level Music Audio Source here")]
    [SerializeField]
    AudioSource levelMusic;
    [SerializeField]
    public Slider timerSlider;
    #endregion

    private void Start()
    {
        StartCoroutine(Countdown());
        //readyToSpawn = false;
        //delayOccurred = false;

        // timer
        totalTime = timer;
        timerActive = true;
        levelMusic.pitch = 1f;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        halfTime = (int)(totalTime / 2);

        if (uIManager == null)
        {
            Debug.Log("UIManager has not been assigned in GameManager's Inspector");
        }
    }

    private void Update()
    {
        #region litterUpdate
        //if (litterParent != null && litter != null)
        //{
        //    if (!delayOccurred)
        //    {
        //        StartCoroutine(SpawnStartDelay());
        //    }

        //    if (readyToSpawn)
        //    {
        //        readyToSpawn = false;
        //        InstantiateItem();
        //        RI.EIMScore(false);
        //    }
        //}

        //else { Debug.Log("Please assign litter prefab and/or litterParent into the hierarchy on GameManager script"); }
        #endregion

        #region timerUpdate
        if (timerActive)
        {
            if (timer > 0)
            {
                Timer();
                TimeToInt();
                timerSlider.value = timer;


                if (timeAsInt == halfTime && !thresh1)
                {
                    TimerSFX(1);
                }

                else if (timeAsInt == 60 && !thresh2) // 30
                {
                    TimerSFX(2);
                }

                //else if (timeAsInt == 45 && !thresh3) // 15
                //{
                //    TimerSFX(3);
                //}   
            }

            else
            {
                timer = 0;
                timerActive = false;
                uIManager.WinOrLose(false);
            }

            #region timerspare
            //if (timer == totalTime/2f)
            //{
            //    Debug.Log("Time threshold 1 reached");
            //    // truck noise, visual
            //    uIManager.PulseScaleAnim(2f);
            //    levelMusic.pitch = 1.19f;
            //}

            //// these two aren't called
            //if (timer == 30)
            //{
            //    Debug.Log("Time threshold 2 reached");
            //    // truck noise, visual
            //    uIManager.PulseScaleAnim(2f);
            //    levelMusic.pitch = 1.31f;
            //}

            //if (timer == 15)
            //{
            //    Debug.Log("Time threshold 3 reached");
            //    // truck noise, visual
            //    uIManager.PulseScaleAnim(2f);
            //    levelMusic.pitch = 1.56f;
            //}

            //if (timer == totalTime / 2f)
            //{

            //    Debug.Log("Time threshold 1 reached");
            //    // truck noise, visual
            //    //uIManager.PulseScaleAnim(2f);
            //    TimerSFX(1);
            //    //levelMusic.pitch = 1.19f;
            //}

            //// these two aren't called
            //else if (timer == 30)
            //{
            //    Debug.Log("Time threshold 2 reached");
            //    // truck noise, visual
            //    //uIManager.PulseScaleAnim(2f);
            //    TimerSFX(2);
            //   // levelMusic.pitch = 1.31f;
            //}

            //else if (timer == 15)
            //{
            //    Debug.Log("Time threshold 3 reached");
            //    // truck noise, visual
            //    //uIManager.PulseScaleAnim(2f);
            //    TimerSFX(3);
            //   // levelMusic.pitch = 1.56f;
            //}
        }
        #endregion
        #endregion

    }

    #region itemSpawnMethods

    // beca note: why won't this summary show?
    /// <summary>
    /// Generates a random X & Z point from the limits set in the inspector & takes the input from the inspector for the Y axis
    /// then generates a random piece of litter to be spawned
    /// </summary>
    /// <returns></returns>
    //private void InstantiateItem()
    //{
    //    float rX = Random.Range(xMin, xMax);
    //    float rZ = Random.Range(zMin, zMax);
    //    litterPos = new Vector3 (rX, litterPosY, rZ);

    //    // which piece of litter is to be spawned
    //    int num = Random.Range(0, litter.Length);
    //    //Debug.Log(litter[num]);

    //    Instantiate(litter[num], litterPos, Quaternion.identity, litterParent);
    //    StartCoroutine(SpawnTimer());
    //}

    /// <summary>
    /// How often litter should spawn
    /// </summary>
    /// <returns></returns>
    //private IEnumerator SpawnTimer()
    //{
    //    yield return new WaitForSeconds(litterSpawnTime);
    //    readyToSpawn = true;
    //}

    /// <summary>
    /// How long before items should start spawning into the scene after startup
    /// </summary>
    /// <returns></returns>
    //private IEnumerator SpawnStartDelay()
    //{
    //    delayOccurred = true;
    //    yield return new WaitForSeconds(startDelay);
    //    readyToSpawn = true;
    //}
    #endregion

    IEnumerator Countdown()
    {
        // Pause the game's time
        Time.timeScale = 0f;

        countdownText.text = "3";
        threeSfxSource.PlayOneShot(threeSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "2";
        twoSfxSource.PlayOneShot(twoSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        oneSfxSource.PlayOneShot(oneSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "Go!";
        goSfxSource.PlayOneShot(goSFX);
        yield return new WaitForSecondsRealtime(1f);
        

        // Resume the game's time
        countdownText.text = "";
        Time.timeScale = 1f;
    }

        // accesses value set for binsMaxCapacity 
    public int MaxCapacity()
    {
        return binsMaxCapacity;
    }

    #region timerMethods
    private float Timer()
    {
        return timer -= Time.deltaTime;
    }

    private void TimerSFX(int threshold)
    {
        uIManager.TruckTimeThresholdAnim(); 

        switch (threshold)
        {
            case 1:
                truckSource.PlayOneShot(truckClip);
                levelMusic.pitch = 1.19f;
                thresh1 = true;
                break;

            case 2:
                truckSource.PlayOneShot(truckClip);
                levelMusic.pitch = 1.31f;
                thresh2 = true;
                break;

            case 3:
                truckSource.PlayOneShot(truckClip);
                levelMusic.pitch = 1.56f;
                thresh3 = true;
                break;

                // default?
        }
    }

    /// <summary>
    /// Public method for other scripts to access the Timer's current time
    /// </summary>
    /// <returns></returns>
    public float CurrentTime()
    {
        return timer;
    }

    // converts time type from float to int
    private void TimeToInt()
    {
        timeAsInt = Mathf.FloorToInt(timer);
    }

    // will start the timer from the time set in the inspector 
    public void StartTimer()
    {

    }

    // will reset the timer to the time set in the inspector 
    public void ResetTimer()
    {
        timer = totalTime;
    }

    public bool IsTimeUp()
    {
        return timer <= 0;
    }

    #endregion
}

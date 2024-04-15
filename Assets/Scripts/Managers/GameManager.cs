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

    // allows for accurate comparison of ints when using time-based thresholds for the truck timer slider
    private int timeAsInt;

    [Header("Truck Handler (Audio)")]

    // -------------------
    // make the 'truck' audio play once
    private bool thresh1;
    private bool thresh2;
    private bool thresh3;
    // -------------------

    [SerializeField]
    private AudioSource truckSource;

    [SerializeField]
    private AudioClip truckClip;

    [Header("Drag UIManager GameObject Here")]
    [SerializeField]
    private UIManager uIManager;

    // saves the time alotted in the inspector to reset the timer
    private float totalTime;

    // holds half of the current levels time
    private int halfTime;

    // is the timer active
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

                else if (timeAsInt == 30 && !thresh2)
                {
                    TimerSFX(2);
                }

                else if (timeAsInt == 15 && !thresh3)
                {
                    TimerSFX(3);
                }
            }

            else
            {
                timer = 0;
                timerActive = false;
                uIManager.WinOrLose(false);
            }
        }
        #endregion
    }

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
        levelMusic.Play();
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
        uIManager.StartCoroutine("ThresholdAnim"); // make variable to hold coroutine?

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

    // converts time type from float to int
    private void TimeToInt()
    {
        timeAsInt = Mathf.FloorToInt(timer);
    }

    #endregion
}

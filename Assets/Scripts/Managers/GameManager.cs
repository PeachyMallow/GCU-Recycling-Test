using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Drag RubbishInteraction script here\n(currently on Player)")]
    [SerializeField]
    private RubbishInteraction RI;

    #region Starting Countdown
    [Header("Drag Countdown Splash here")]
    [SerializeField]
    public GameObject countdownSplashGO;
    [SerializeField]
    public Sprite countdownThreeImage;
    [SerializeField]
    public Sprite countdownTwoImage;
    [SerializeField]
    public Sprite countdownOneImage;
    [SerializeField]
    public Sprite countdownGoImage;
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

    // prevents pausing during countdown
    private bool countdownFinished;

    #endregion

    [Header("\n----------------------------\n\n\nBin Capacity\n")]

    [Header("Max Capacity of each bin")]
    [SerializeField]
    private int binsMaxCapacity;

    #region timerVariables
    [Header("\n----------------------------\n\n\nTimer\n")]
    
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
    #endregion

    //Audio Management with Timer
    [Header("Drag Level Music Audio Source here")]
    [SerializeField]
    AudioSource levelMusic;

    [SerializeField]
    public Slider timerSlider;

    private void Start()
    {
        countdownFinished = false;
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

        countdownSplashGO.GetComponent<Image>().sprite = countdownThreeImage;
        threeSfxSource.PlayOneShot(threeSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownSplashGO.GetComponent<Image>().sprite = countdownTwoImage;
        twoSfxSource.PlayOneShot(twoSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownSplashGO.GetComponent<Image>().sprite = countdownOneImage;
        oneSfxSource.PlayOneShot(oneSFX);
        yield return new WaitForSecondsRealtime(1f);

        countdownSplashGO.GetComponent<Image>().sprite = countdownGoImage;
        goSfxSource.PlayOneShot(goSFX);
        yield return new WaitForSecondsRealtime(1f);


        // Resume the game's time
        countdownSplashGO.SetActive(false);
        Time.timeScale = 1f;
        levelMusic.Play();
        countdownFinished = true;
    }

    /// <summary>
    /// Public bool to access if the start of level countdown has finished
    /// true - countdown has finished
    /// false - countdown has not finished
    /// </summary>
    /// <returns></returns>
    public bool HasCountdownFinished()
    {
        if (countdownFinished)
        {
            return true;
        }

        else { return false; }
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
        }
    }

    // converts time type from float to int
    private void TimeToInt()
    {
        timeAsInt = Mathf.FloorToInt(timer);
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;
using Unity.Services.Analytics;
using UnityEngine.Analytics;
using Unity.Services.Core;

public class RubbishInteraction : MonoBehaviour
{
    public Text RubbishScore;
    public Text score;
    private int recycledScore;
    public int currentRecycledScore;
    private int recycledHighScore;
    private int numRubbish;
    private int numRubbishHeld;
    public float radNum = 0f;
    float currentVelocity = 0;
    public Vector3 collision = Vector3.zero;

    [Header("SFX Here")]
    [SerializeField]
    public AudioSource pickupSource;
    public AudioClip pickupClip;

    private bool isGameOver;
    //public GameObject victoryMenuUI;
    //public GameObject gameOverMenuUI;
    private GameObject Menu;
    public GameObject lastHit;

    [SerializeField]
    private bool Autopickup;

    [SerializeField]
    public Slider enviroMeter;
    [SerializeField]
    [Header("Drag UI Glow Here")]
    private CanvasGroup increaseGlowGroup;
    [SerializeField]
    private CanvasGroup decreaseGlowGroup;
    [SerializeField]
    private float fadeSpeed;
    private bool increaseFadeIn = false;
    private bool increaseFadeOut = true;
    private bool decreaseFadeIn = true;
    private bool decreaseFadeOut = true;
    [SerializeField]
    private AudioSource increaseSource;
    [SerializeField]
    private AudioClip increaseClip;
    [SerializeField]
    private AudioSource decreaseSource;
    [SerializeField]
    private AudioClip decreaseClip;

    // used to update what the player is currently holding
    //[Header("Drag PlayerManager GameObject into here")]
    //[SerializeField]
    //private PlayerManager playerManager;

    [Header("Drag UIManager GameObject into here")]
    [SerializeField]
    private UIManager uiManager;

    // true when player presses E
    private bool keyPressed;

    // required to deposit one item at a time
    private bool canDeposit;



    void Start()
    {
        //AnalyticsService.Instance.RecordEvent(myEvent);




        //Unity.Services.Analytics.CustomEvent();

        //Analytics.CustomEvent("EventName", new Dictionary<string, object>()
        //{
        //    { "property 1", 99 },
        //    { "property 2", "Green bin"},
        //});

        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        //recycledScore = 5;
        ResetScore();
        recycledHighScore = recycledScore;
        enviroMeter.value = recycledScore;
        Console.WriteLine("Auto Pickup Active");
        keyPressed = false;
        canDeposit = false;
        increaseFadeIn = false;
        decreaseFadeIn = false;
    }

    private void Update()
    {
        // when the player is depositing rubbish
        if (Input.GetKeyDown(KeyCode.E))
        {
            canDeposit = true;
            keyPressed = true;
        }

        //once the player has deposited a piece of rubbish
        else if (Input.GetKeyUp(KeyCode.E))
        {
            keyPressed = false;
            canDeposit = false;   
        }

        PickupSwitch();
        //EndingmenuUI();

        float currentScore = Mathf.SmoothDamp(enviroMeter.value, recycledScore, ref currentVelocity, 100 * Time.deltaTime);
        enviroMeter.value = currentScore;

        if (increaseFadeIn == true)
        {
            if (increaseGlowGroup.alpha < 1)
            {
                increaseGlowGroup.alpha += Time.deltaTime * fadeSpeed;
                if (increaseGlowGroup.alpha >= 1)
                {
                    increaseFadeIn = false;
                    increaseSource.PlayOneShot(increaseClip);
                    increaseFadeOut = true;
                }
            }
        }

        if (increaseFadeOut == true)
        {
            if (increaseGlowGroup.alpha >= 0)
            {
                increaseGlowGroup.alpha -= Time.deltaTime * fadeSpeed;
                if (increaseGlowGroup.alpha == 0)
                {
                    increaseFadeOut = false;
                }
            }
        }

        if (decreaseFadeIn == true)
        {
            if (decreaseGlowGroup.alpha < 1)
            {
                decreaseGlowGroup.alpha += Time.deltaTime * fadeSpeed;
                if (decreaseGlowGroup.alpha >= 1)
                {
                    decreaseFadeIn = false;
                    decreaseSource.PlayOneShot(decreaseClip);
                    decreaseFadeOut = true;
                }
            }
        }

        if (decreaseFadeOut == true)
        {
            if (decreaseGlowGroup.alpha >= 0)
            {
                decreaseGlowGroup.alpha -= Time.deltaTime * fadeSpeed;
                if (decreaseGlowGroup.alpha == 0)
                {
                    decreaseFadeOut = false;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        float raycastLength = 14f; // Adjust the ray length here 
        Vector3 raycastOrigin = transform.position + Vector3.up * 10; // Adjust the ray height here
        Vector3 raycastDirection = transform.forward; // set ray direction

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastLength))
        {
            if (hit.collider.CompareTag("Bin") && keyPressed)
            {
                Debug.Log(hit.collider.name);
               // canDeposit = true;
            }
            else
            {
                //canDeposit = false;
            }
            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(raycastOrigin, raycastDirection * raycastLength, Color.red);
            canDeposit = false;
        }
    }

    private void OnTriggerEnter(Collider Rubbish)
    {
        if (Autopickup == true)
        {
            if (Rubbish.tag == "NonRecyclable" || Rubbish.tag == "Paper" || Rubbish.tag == "LiquidInside" || Rubbish.tag == "FoodWaste" || Rubbish.tag == "Plastic")
            {
                RubbishPickup(Rubbish.gameObject);
            }
        }
        else { }

    }

    // these check if the player is in contact with the bins allowing for a deposit 
    private void OnTriggerStay(Collider RubbishBin)
    {
        if (RubbishBin.tag == "Bin")
        {
            // is the bin is full?
            if (!RubbishBin.GetComponent<Bins>().IsBinFull())
            {
                // keypress 'E' is controlled in Update()
                if (keyPressed && Inventory.instance.InventorySize() > 0 && canDeposit)
                {
                    /*// unsure if needed?
                    //recycledScore++;
                    //recycledHighScore++;*/

                    // Deposit rubbish into the bin
                    Inventory.instance.Remove(uiManager.GetInventoryPos(), RubbishBin.gameObject);

                    // Trigger analytics event for depositing rubbish into the incorrect bin
                    FindObjectOfType<UGS_Analytics>().IncorrectPaperBinDepositEvent(RubbishBin.name,"Paper");
                    // Record the custom event for depositing rubbish
                    //RecordDepositEvent(RubbishBin.name);

                    //Inventory.instance.Remove(uiManager.GetInventoryPos(), RubbishBin.gameObject);

                    //numRubbishHeld = Inventory.instance.InventorySize();

                    /*//Debug.Log("Score: " + recycledScore);
                    // unsure if needed?
                    score.text = "Rubbish Recycled : " + recycledHighScore;
                    enviroMeter.value = recycledScore;
                    RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;*/
                }

                else if (keyPressed && numRubbish <= 0)
                {
                    Console.WriteLine("No rubbish to deposit");
                }
            }

            canDeposit = false;
        }

        if (Autopickup == false)
        {
            if (RubbishBin.tag == "NonRecyclable" || RubbishBin.tag == "Paper" || RubbishBin.tag == "LiquidInside" || RubbishBin.tag == "FoodWaste" || RubbishBin.tag == "Plastic" && Input.GetKey(KeyCode.E))
            {
                RubbishPickup(RubbishBin.gameObject);
            }
        }
    }
    //// Method to record a custom event for depositing rubbish
    //private void RecordDepositEvent(string binName)
    //{
    //    // Define the parameters for the custom event
    //    var eventParams = new Dictionary<string, object>
    //    {
    //        { "BinName", binName }
    //        // Add more parameters if needed
    //    };

    //    // Record the custom event
    //    Analytics.CustomEvent("RubbishDeposited", eventParams);
    //}

    /// <summary>
    /// If player's inventory is not full, then player will pick up rubbish
    /// </summary>
    /// <param name="Rubbish"></param>
    private void RubbishPickup(GameObject Rubbish)
    {
        if (!Inventory.instance.IsInventoryFull())
        {
            numRubbish++;
            numRubbishHeld++;
            Inventory.instance.Add(Rubbish.GetComponent<Pickup>().item);
            pickupSource.PlayOneShot(pickupClip);
            Rubbish.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes EIM Score dependant on player interaction with the bin
    /// if a is true, then increase the score
    /// if a is false, then decrease the score
    /// </summary>
    public void EIMScore(bool a) // this should be where analytics are called
    {
        if (a)
        {
            if (recycledScore > 0)
            {
                recycledScore++;
                enviroMeter.value = recycledScore;
                increaseFadeIn = true;


            }
        }

        else
        {
            if (recycledScore > 0)
            {
                recycledScore--;
                enviroMeter.value = recycledScore;
                decreaseFadeIn = true;
            }
        }

        ScoreCheck();
    }

    /// <summary>
    /// Checks if player has reached win or lose threshold everytime they gain/lose a point
    /// </summary>
    private void ScoreCheck()
    {
        if (recycledScore >= 10)
        {
            uiManager.WinOrLose(true);
        }

        else if (recycledScore <= 0)
        {
            uiManager.WinOrLose(false);
        }
    }

    //public void EndingmenuUI()
    //{
    //    if (enviroMeter.value == 10)
    //    {
    //        victoryMenuUI.SetActive(true);
    //        Time.timeScale = 0f;
    //    }

    //    else if (enviroMeter.value == 0)
    //    {
    //        gameOverMenuUI.SetActive(true);
    //        Time.timeScale = 0f;
    //    }
    //}

    /// <summary>
    /// Returns player's current score
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return recycledScore;
    }

    /// <summary>
    /// Resets player's score to 5
    /// </summary>
    /// <param name="score"></param>
    public void ResetScore()
    {
        recycledScore = 5;
    }

    public void Continue()
    {
        // Ui Buttons Functions
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        // Ui Buttons Functions
    }

    // used to Switch between manual and automatic pickup
    private void PickupSwitch()
    {
        if (Autopickup == true && Input.GetKey(KeyCode.Q))
        {
            Autopickup = false;
            Debug.Log("Key Pickup Active");
        }
        else if (Autopickup == false && Input.GetKey(KeyCode.Q))
        {
            Autopickup = true;
            Debug.Log("Auto Pickup Active");
        }
    }
}
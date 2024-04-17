using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;

public class RubbishInteraction : MonoBehaviour
{
    public Text RubbishScore;
    public Text score;
    [SerializeField]
    public int recycledScore;
    [SerializeField]
    public int currentRecycledScore;
    private int recycledHighScore;
    public int displayScore;
    private int numRubbish;
    private int numRubbishHeld;
    public float radNum = 0f;
    float currentVelocity = 0;
    public Vector3 collision = Vector3.zero;
    [SerializeField]
    public Animator binAnimator;

    [SerializeField]
    public bool binShakeBool;

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

    #region All UI Variables
    //[SerializeField]
    // public Slider enviroMeter;

    [SerializeField]
    private GameObject depositIcon;
    [SerializeField]
    private AudioSource increaseSource;
    [SerializeField]
    private AudioClip increaseClip;
    [SerializeField]
    private AudioSource decreaseSource;
    [SerializeField]
    private AudioClip decreaseClip;

    #endregion

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
    public GameObject Player;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        Autopickup = true;
        numRubbish = 0;
        numRubbishHeld = 0;
        //recycledScore = 5;
        ResetScore();
        recycledHighScore = recycledScore;
        //enviroMeter.value = recycledScore;
        Console.WriteLine("Auto Pickup Active");
        keyPressed = false;
        canDeposit = false;
    }

    private void Update()
    {
        // when the player is depositing rubbish
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canDeposit = true;
            keyPressed = true;
        }

        //once the player has deposited a piece of rubbish
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            keyPressed = false;
            canDeposit = false;
        }

        PickupSwitch();
        //EndingmenuUI();

        float currentScore = Mathf.SmoothDamp(0, recycledScore, ref currentVelocity, 100 * Time.deltaTime);

        #region Star UI Management
        if (recycledScore >= uiManager.starOneThresh)
        {
            uiManager.starOne.GetComponent<Image>().sprite = uiManager.starFull;
        }

        if (recycledScore <= uiManager.starOneThresh)
        {
            uiManager.starOne.GetComponent<Image>().sprite = uiManager.starEmpty;
        }

        if (recycledScore >= uiManager.starTwoThresh)
        {
           uiManager.starTwo.GetComponent<Image>().sprite = uiManager.starFull;
        }

        if (recycledScore <= uiManager.starTwoThresh)
        {
            uiManager.starTwo.GetComponent<Image>().sprite = uiManager.starEmpty;
        }

        if (recycledScore >= uiManager.starThreeThresh)
        {
           uiManager.starThree.GetComponent<Image>().sprite = uiManager.starFull;
        }

        if (recycledScore <= uiManager.starThreeThresh)
        {
            uiManager.starThree.GetComponent<Image>().sprite = uiManager.starEmpty;
        }
        #endregion
  
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        float raycastLength = 14f; // Adjust the ray length here 
        Vector3 raycastOrigin = transform.position + Vector3.up * 5; // Adjust the ray height here
        Vector3 raycastDirection = transform.forward; // set ray direction

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastLength))
        {
            if (hit.collider.CompareTag("Bin") && keyPressed)
            {
                //Debug.Log(hit.collider.name);
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
            depositIcon.SetActive(false);
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
                // binAnimator = RubbishBin.GetComponent<Animator>();
                binAnimator.SetBool("binShakingBool", true);
                depositIcon.SetActive(true);
                // keypress 'E' is controlled in Update()
                if (keyPressed && Inventory.instance.InventorySize() > 0 && canDeposit)
                {
                    /*// unsure if needed?
                    //recycledScore++;
                    //recycledHighScore++;*/
                    GetComponent<Animator>().Play("Deposit", -1, 0f);
                    Inventory.instance.Remove(uiManager.GetInventoryPos(), RubbishBin.gameObject);

                    numRubbishHeld = Inventory.instance.InventorySize();

                    /*//Debug.Log("Score: " + recycledScore);
                    // unsure if needed?
                    score.text = "Rubbish Recycled : " + recycledHighScore;
                    enviroMeter.value = recycledScore;
                    RubbishScore.text = "Rubbish Collected: " + numRubbishHeld;*/
                }

                else if (keyPressed && numRubbish <= 0)
                {
                    Console.WriteLine("No rubbish to deposit");
                    //animator.SetBool("isRecycling", false);
                }

                if (numRubbish <= 0)
                {
                    binAnimator.SetBool("binShakingBool", false);
                    depositIcon.SetActive(false);
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
    /// Changes Score dependant on player interaction with the bin
    /// if a is true, then increase the score
    /// if a is false, then decrease the score
    /// </summary>
    public void Score(bool a)
    {
        if (a)
        {
            recycledScore++;
            // enviroMeter.value = recycledScore;
            
            StartCoroutine(uiManager.ScoreDepositGlow(true));
        }

        else
        {
            recycledScore--;
            // enviroMeter.value = recycledScore;
            
            StartCoroutine(uiManager.ScoreDepositGlow(false));
        }
    }

    /// <summary>
    /// Checks if player has reached win or lose threshold everytime they gain/lose a point
    /// </summary>


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
        recycledScore = 1;
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
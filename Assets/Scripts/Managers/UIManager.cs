using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
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

    #region STARS
    [SerializeField]
    [Header("Drag UI Assets Here")]
    public GameObject winUI;
    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;
    public Sprite starFull;
    public Sprite starEmpty;

    [Header("Debug for Stars and End Fanfare")]
    public bool winScreenActive;
    public float starOneDelay;
    public float starTwoDelay;
    public float starThreeDelay;
    public float fanfareDelay;
    public bool hasStarOnePlayed = false;
    public bool hasStarTwoPlayed = false;
    public bool hasStarThreePlayed = false;
    public bool hasFanfarePlayed = false;
   

    [Header("Drag Animators on Stars Here")]
    [SerializeField]
    public Animator starOneAnimator;
    [SerializeField]
    public Animator starTwoAnimator;
    [SerializeField]
    public Animator starThreeAnimator;

    [Header("Debug for Star Threshold")]
    [Header("Enter Value as ORIGINAL - NOT x25")]
    [SerializeField]
    public float starOneThresh;
    public float starTwoThresh;
    public float starThreeThresh;
    public float starsEarned;

    [Header("Win Screen Audio")]
    [SerializeField]
    public AudioSource drumRollSource;
    public AudioClip drumRollSFX;
    public AudioSource starOneSource;
    public AudioClip starOneSFX;
    public AudioSource starTwoSource;
    public AudioClip starTwoSFX;
    public AudioSource starThreeSource;
    public AudioClip starThreeSFX;
    [Header("Fanfare Audio")]
    public AudioSource oneStarFanfareSource;
    public AudioClip oneStarFanfareClip;
    public AudioSource twoStarFanfareSource;
    public AudioClip twoStarFanfareClip;
    public AudioSource threeStarFanfareSource;
    public AudioClip threeStarFanfareClip;

    #endregion
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



    [Header("\n----------------------------\n\n\nScore Glow\n")]

    [Header("Drag 'ScoreGlowImg' here")]
    [SerializeField]
    private Image glowImage;

    [Header("Set the glow's goal opacity here")]
    [SerializeField]
    private float maxGlowTransparency;

    [Header("Set how fast the glow should fade in/out here")]
    [SerializeField]
    private float fadeSpeed;

    [Header("Set correct & incorrect deposit colours here")]
    [SerializeField]
    private Color correctDepositColour;
    [SerializeField]
    private Color incorrectDepositColour;

    // set in Start() - AudioSource component of 'glowImage'
    private AudioSource scoreAudioSource;

    [Header("Add Score Audio Clips here")]
    [SerializeField]
    private AudioClip scoreIncreaseAudio;
    [SerializeField]
    private AudioClip scoreDecreaseAudio;

    private bool glowInProgress;
    private int glowCounter;
    private Color currentColour;
    private Image currentImage;

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

        // score
        scoreAudioSource = glowImage.GetComponent<AudioSource>();

        // win screen
        winScreenActive = false;
    }

    private void Update()
    {
        // pause screen
        if (pauseMenu != null)
        {
            // prevents pausing during the start of level countdown
            if (gM.HasCountdownFinished())
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    TogglePause();
                }
            }
        }

        // scrolling hotbar
        if (Input.GetKeyDown(KeyCode.H)) // left
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

        if (Input.GetKeyDown(KeyCode.J)) // right
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

        // Checking if the WinScreen object is active for Star Animation
        if (winScreenActive)
        {
            // Decrease delay for each star only if its animation hasn't played yet
            if (!hasStarOnePlayed)
            {
                starOneDelay -= Time.unscaledDeltaTime;
                // Debug.Log("Star One Delay: " + starOneDelay);
            }

            if (!hasStarTwoPlayed)
            {
                starTwoDelay -= Time.unscaledDeltaTime;
                //Debug.Log("Star Two Delay: " + starTwoDelay);
            }

            if (!hasStarThreePlayed)
            {
                starThreeDelay -= Time.unscaledDeltaTime;
                // Debug.Log("Star Three Delay: " + starThreeDelay);
            }
            if (!hasFanfarePlayed)
            {
                fanfareDelay -= Time.unscaledDeltaTime;
            }

            if (starOneDelay <= 0 && !hasStarOnePlayed)
            {
                Debug.Log("Playing Star One Animation");
                PlayStarAnimation(starOne, "PlayStarOne", ref hasStarOnePlayed, starOneSource, starOneSFX);
            }

            if (starTwoDelay <= 0 && !hasStarTwoPlayed)
            {
                Debug.Log("Playing Star Two Animation");
                PlayStarAnimation(starTwo, "PlayStarTwo", ref hasStarTwoPlayed, starTwoSource, starTwoSFX);
            }

            if (starThreeDelay <= 0 && !hasStarThreePlayed)
            {
                Debug.Log("Playing Star Three Animation");
                PlayStarAnimation(starThree, "PlayStarThree", ref hasStarThreePlayed, starThreeSource, starThreeSFX);
            }

            if (fanfareDelay <= 0 && !hasFanfarePlayed)
            {
                PlayEndFanfare(starsEarned);
                hasFanfarePlayed = true;
            }
        }


    }

    /// <summary>
    /// Changes 'Score Glow' dependant on if the bool passed is true or false
    /// true - green glow (set in inspector 'correctDepositColour')
    /// false - red glow (set in inspector 'incorrectDepositColour')
    /// </summary>
    /// <param name="correctDeposit"></param>
    /// <returns></returns>
    public IEnumerator ScoreDepositGlow(bool correctDeposit)
    {
        // sets the corresponding score glow colour
        if (correctDeposit) // green
        {
            currentColour = correctDepositColour;
            scoreAudioSource.PlayOneShot(scoreIncreaseAudio);
        }

        else // red
        {
            currentColour = incorrectDepositColour;
            scoreAudioSource.PlayOneShot(scoreDecreaseAudio);
        }

        currentImage = glowImage;

        float targetAlpha = maxGlowTransparency;

        if (!glowInProgress)
        {
            glowInProgress = true;

            while (glowCounter < 2)
            {
                while (Mathf.Abs(currentColour.a - targetAlpha) > 0.005f)
                {
                    currentColour.a = Mathf.Lerp(currentColour.a, targetAlpha, fadeSpeed * Time.deltaTime);
                    currentImage.color = currentColour;
                    yield return null;
                }

                targetAlpha = 0f;
                glowCounter++;
            }

            // reset: glow transparency to 0
            currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, 0.0f);
            glowInProgress = false;
            glowCounter = 0;
        }
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

    #region pauseScreenMethods
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
    #endregion

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

                winScreenActive = true;
                winUI.SetActive(true);
                Time.timeScale = 0.0f;
            }

            else { Debug.Log("Victory screen has not been assigned in UIManager Inspector"); }
        }

        else
        {
            if (gameOverUI != null)
            {

                winScreenActive = true;
                gameOverUI.SetActive(true);
                drumRollSource.PlayOneShot(drumRollSFX);
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

    private void PlayStarAnimation(GameObject star, string triggerName, ref bool hasPlayed, AudioSource starSource, AudioClip starClip)
    {
        if (star != null)
        {
            Animator starAnimator = star.GetComponent<Animator>();
            if (starAnimator != null && rubbishInteraction.recycledScore >= GetStarThreshold(star))
            {
                Debug.Log("Triggering Animation for " + star.name + " with trigger: " + triggerName);
                starAnimator.SetTrigger(triggerName);
                hasPlayed = true;
                starSource.PlayOneShot(starClip);
                starsEarned++;

            }
        }
    }

    private void PlayEndFanfare(float totalStars)
    {
        if (totalStars == 1)
        {
            oneStarFanfareSource.PlayOneShot(oneStarFanfareClip);
        }

        if (totalStars == 2)
        {
            twoStarFanfareSource.PlayOneShot(twoStarFanfareClip);
        }

        if (totalStars == 3)
        {
            threeStarFanfareSource.PlayOneShot(threeStarFanfareClip);
        }
    }


    private float GetStarThreshold(GameObject star)
    {
        if (star == starOne)
            return starOneThresh;
        else if (star == starTwo)
            return starTwoThresh;
        else if (star == starThree)
            return starThreeThresh;

        // Return a default threshold value if the star is not recognized
        return float.MaxValue; // Return a very large value
    }
}

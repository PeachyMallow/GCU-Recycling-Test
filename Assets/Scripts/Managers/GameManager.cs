using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

// beca note: create one 'timer method' that's not exclusive to the Timer
public class GameManager : MonoBehaviour
{
    #region timerVariables
    [Header("\nTimer\n")]
    // timer set in inspector
    [Header("Enter time in seconds")]
    [SerializeField]
    private float timer;

    //// saves the timer internally
    //[SerializeField]
    //private float timer;

    // saves the time alotted in the inspector to reset the timer
    private float totalTime;

    // is the timer active
    private bool timerActive;
    #endregion

    #region itemSpawnVariables
    [Header("----------------------------\n\nLitter Spawn\n")]

    // litter's parent
    [Header("Drag Litter Parent GameObject here")]
    [SerializeField]
    private Transform litterParent;

    // item spawn
    [Header("Drag litter prefabs here\nOnly items added here will spawn")]
    [SerializeField]
    private GameObject[] litter;

    // time before item spawn should begin
    [Header("Time Before Item Spawn Should Begin")]
    [SerializeField]
    private float startDelay;

    // true once the start delay has been called
    private bool delayOccurred;

    // time between items spawning
    [Header("Time in seconds for litter spawning")]
    [SerializeField]
    private float litterSpawnTime;

    // y position of litter to be instantiated
    [Header("Point on Y axis litter should spawn")]
    [SerializeField]
    private float litterPosY;

    // beca note: could change these to be some type of co-ordinate?
    [Header("The Area Where the Litter Should Spawn")]
    [Header("X Min")]
    [SerializeField]
    private int xMin;

    [Header("X Max")]
    [SerializeField]
    private int xMax;

    [Header("Z Min")]
    [SerializeField]
    private int zMin;

    [Header("Z Max")]
    [SerializeField]
    private int zMax;

    // temp variables
    [Header("For Debugging")]
    // position of litter to be instantiated
    [Header("Position for litter to spawn")]
    [SerializeField]
    private Vector3 litterPos;

    // true if there is an item has been spawned
    [SerializeField]
    private bool readyToSpawn;
    #endregion

    private void Start()
    {
        totalTime = timer;
        timerActive = true;
        readyToSpawn = false;
        delayOccurred = false;
    }

    private void Update()
    {
        #region timerUpdate
        if (timerActive)
        {
            if (timer > 0)
            {
                Timer();
            }

            else
            {
                timer = 0;
                Debug.Log("Time's Up!");
                timerActive = false;
            }
        }
        #endregion

        #region litterUpdate
        if (litterParent != null && litter != null)
        {
            if (!delayOccurred)
            {
                StartCoroutine(SpawnStartDelay());
            }

            if (readyToSpawn)
            {
                readyToSpawn = false;
                InstantiateItem();
            }
        }

        else { Debug.Log("Please assign litter prefab and/or litterParent into the hierarchy on GameManager script"); }
        #endregion
    }

    #region timerMethods
    private float Timer()
    {
        return timer -= Time.deltaTime;
    }

    /// <summary>
    /// Public method for other scripts to access the Timer's current time
    /// </summary>
    /// <returns></returns>
    public float CurrentTime()
    {
        return timer;
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

    #region itemSpawnMethods

    // beca note: why won't this summary show?
    /// <summary>
    /// Generates a random X & Z point from the limits set in the inspector & takes the input from the inspector for the Y axis
    /// then generates a random piece of litter to be spawned
    /// </summary>
    /// <returns></returns>
    private void InstantiateItem()
    {
        float rX = Random.Range(xMin, xMax);
        float rZ = Random.Range(zMin, zMax);
        litterPos = new Vector3 (rX, litterPosY, rZ);

        // which piece of litter is to be spawned
        int num = Random.Range(0, litter.Length);
        //Debug.Log(litter[num]);

        Instantiate(litter[num], litterPos, Quaternion.identity, litterParent);
        StartCoroutine(SpawnTimer());
    }

    /// <summary>
    /// How often litter should spawn
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(litterSpawnTime);
        readyToSpawn = true;
    }

    /// <summary>
    /// How long before items should start spawning into the scene after startup
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnStartDelay()
    {
        delayOccurred = true;
        yield return new WaitForSeconds(startDelay);
        readyToSpawn = true;
    }
    #endregion
}

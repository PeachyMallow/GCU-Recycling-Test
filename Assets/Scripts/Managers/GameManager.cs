using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region timerVariables
    [Header("\nTimer\n")]
    // timer set in inspector
    [Header("Enter time in seconds")]
    [SerializeField]
    private float timer;

    // saves the time alotted in the inspector to reset the timer
    private float totalTime;

    // is the timer active
    private bool timerActive;
    #endregion

    [Header("\n\nLitter Spawn\n")]
    // item spawn
    [Header("Drag rubbish prefab here")]
    [SerializeField]
    private GameObject[] litter;

    // time between items spawning
    [Header("Time in seconds for litter spawning")]
    [SerializeField]
    private float litterSpawnTime;

    // temp variables

    [Header("Temp Variables")]
    // y position of litter to be instantiated
    [Header("Point on Y axis litter should spawn")]
    [SerializeField]
    private float litterPosY;

    // position of litter to be instantiated
    [Header("Position for litter to spawn")]
    [SerializeField]
    private Vector3 litterPos;

    // litter's parent
    [Header("Drag Litter Parent GameObject here")]
    [SerializeField]
    private Transform litterParent;

    // true if there is an item has been spawned
    [SerializeField]
    private bool readyToSpawn;


    private void Start()
    {
        totalTime = timer;
        timerActive = true;
        readyToSpawn = true;
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

        if (litterParent != null && litter != null)
        {
            if (readyToSpawn)
            {
                readyToSpawn = false;
                InstantiateItem();
            }
        }

        else { Debug.Log("Please assign litter prefab and/or litterParent into the hierarchy on GameManager script"); }

    }

    #region timerMethods
    private float Timer()
    {
        return timer -= Time.deltaTime;
    }

    // public method for other scripts to access the current time 
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

    // item spawn
    // generates a random X & z point and takes the input from the Inspector for the Y
    // generates a random piece of litter to be spawned
    private void InstantiateItem()
    {
        float rX = Random.Range(-13, 13);
        float rZ = Random.Range(-13, 13);
        litterPos = new Vector3 (rX, litterPosY, rZ);

        // which piece of litter is to be spawned
        int num = Random.Range(0, litter.Length);

        Instantiate(litter[num], litterPos, Quaternion.identity, litterParent);
        StartCoroutine(SpawnTimer());
    }

    // timer for item spawn
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(litterSpawnTime);
        readyToSpawn = true;
    }
}

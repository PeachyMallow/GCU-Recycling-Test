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
    private GameObject litter;

    // time between items spawning
    [Header("Time in seconds for litter spawning")]
    [SerializeField]
    private float litterSpawnTime;

    // temp variables
    // position of litter to be instantiated
    [Header("Temp Variables")]
    [Header("Input position for litter to spawn")]
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

        if (readyToSpawn)
        {
            readyToSpawn = false;
            InstantiateItem();
        }
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
    private void InstantiateItem()
    {
        Instantiate(litter, litterPos, Quaternion.identity, litterParent);
        SpawnTimer();
    }

    // timer for item spawn
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(litterSpawnTime);
        readyToSpawn = true;
    }
}

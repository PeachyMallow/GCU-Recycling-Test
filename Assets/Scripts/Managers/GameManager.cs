using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region itemSpawnVariables
    [Header("Litter Spawn\n")]

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
    private bool readyToSpawn;
    #endregion

    [Header("----------------------------\n\nDrag RubbishInteraction script here (currently on Player)")]
    [SerializeField]
    private RubbishInteraction RI;

    // bins
    [Header("----------------------------\n\nBin Capacity\n")]

    [Header("Max Capacity of each bin")]
    [SerializeField]
    private int binsMaxCapacity;

    private void Start()
    {
        readyToSpawn = false;
        delayOccurred = false;
    }

    private void Update()
    {
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
                RI.EIMScore(false);
            }
        }

        else { Debug.Log("Please assign litter prefab and/or litterParent into the hierarchy on GameManager script"); }
        #endregion
    }

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


    // accesses binsMaxCapacity 
    public int MaxCapacity()
    {
        return binsMaxCapacity;
    }
}

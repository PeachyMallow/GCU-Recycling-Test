using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct TargetPoint
{
    public Transform point;
    public float delay;
}

public class NPCNavMesh : MonoBehaviour
{
    [SerializeField] private TargetPoint[] targetPoints; // Array of target points with associated delays
    [SerializeField] private GameObject[] itemsToDrop; // Array of items to drop
    [SerializeField] private float dropDistance = 1.0f; // Distance from NPC's position to drop item
    [SerializeField] private float itemDropDelay = 1.0f; // Delay before dropping another item

    private int currentTargetIndex = 0; // Index to keep track of the current target point
    private bool isWaiting = false; // Flag to indicate if NPC is currently waiting
    private bool isItemDropDelaying = false; // Flag to indicate if NPC is currently delaying item drop
    private GameObject lastDroppedItem; // The last dropped item
    private GameObject itemToDrop; // The item to drop
    private float itemDropTimer = 0f; // Timer for item drop delay

    private NavMeshAgent agent;
    private RubbishInteraction RI;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        RI = FindObjectOfType<RubbishInteraction>();
    }

    private void Start()
    {
        MoveToNextTarget();
    }

    private void Update()
    {
        if (!isWaiting && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitAtTarget(targetPoints[currentTargetIndex].delay));
        }

        // Check if item drop delay is active
        if (isItemDropDelaying)
        {
            itemDropTimer += Time.deltaTime;
            if (itemDropTimer >= itemDropDelay)
            {
                isItemDropDelaying = false;
                itemDropTimer = 0f;
                DropItem();
            }
        }
    }

    private IEnumerator WaitAtTarget(float delay)
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        MoveToNextTarget();
        isWaiting = false;
    }

    private void MoveToNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Length;
        agent.destination = targetPoints[currentTargetIndex].point.position;
        SetRandomItemToDrop();
        StartItemDropDelay();
    }

    /// <summary>
    /// Selects a random item from the array, ensuring it's not the same as the last dropped item
    /// </summary>
    private void SetRandomItemToDrop()
    {
        if (itemsToDrop.Length > 0)
        {
            do
            {
                int randomIndex = Random.Range(0, itemsToDrop.Length);
                itemToDrop = itemsToDrop[randomIndex];
            } while (itemToDrop == lastDroppedItem);

            lastDroppedItem = itemToDrop;
        }
        else
        {
            itemToDrop = null;
        }
    }

    private void StartItemDropDelay()
    {
        isItemDropDelaying = true;
    }

    private void DropItem()
    {
        if (itemToDrop != null)
        {
            Vector3 dropPosition = transform.position + Vector3.up * dropDistance;
            Quaternion itemRotation = Quaternion.identity; // Default rotation (no rotation)

            // Check if the item requires a fixed rotation of -90 degrees
            if (itemToDrop.GetComponent<RequireFixedRotation>() != null)
            {
                itemRotation = Quaternion.Euler(-90f, 0f, 0f); // Fixed rotation of -90 degrees on X-axis
            }

            Instantiate(itemToDrop, dropPosition, itemRotation);
        }
    }

}

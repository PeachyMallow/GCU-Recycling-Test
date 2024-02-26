using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] targetPositions; // Use an array to store multiple target positions
    [SerializeField] private GameObject[] itemsToDrop; // Array of items to drop
    [SerializeField] private float dropDistance = 1.0f; // Distance from NPC's position to drop item
    [SerializeField] private float dropDelay = 1.0f; // Delay before dropping item

    private int currentTargetIndex = 0; // Index to keep track of the current target position
    private bool itemDropped = false; // Flag to keep track if an item has been dropped

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
 
    private void Update()
    {      
        if (targetPositions.Length > 0) // Check if there are any target positions
        {           
            agent.destination = targetPositions[currentTargetIndex].position; // Set the current target position as the agent's destination

            if (agent.remainingDistance <= agent.stoppingDistance) // Check if the agent has reached the current target position
            {
                currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length; // Move to the next target position

                if (!itemDropped) // Drop item if it hasn't been dropped yet
                {
                    StartCoroutine(DropItemWithDelay()); // Start the coroutine to drop item after delay
                }
            }
        }
    }

    private IEnumerator DropItemWithDelay()
    {
        itemDropped = true; // Set the item dropped flag to true

        yield return new WaitForSeconds(dropDelay); // Wait for the specified delay

        if (itemsToDrop.Length > 0)  // If there are items in the array
        {
            // Randomly select an index from the array
            int randomIndex = Random.Range(0, itemsToDrop.Length);
            GameObject itemToDrop = itemsToDrop[randomIndex];

            // Instantiate the selected item at the NPC's position
            Vector3 dropPosition = transform.position + Vector3.up * dropDistance;
            Instantiate(itemToDrop, dropPosition, Quaternion.identity);
        }

        itemDropped = false; // Reset the item dropped flag after dropping
    }
}

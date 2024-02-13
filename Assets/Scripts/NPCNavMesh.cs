using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] targetPositions; // Use an array to store multiple target positions
    private int currentTargetIndex = 0; // Index to keep track of the current target position

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check if there are any target positions
        if (targetPositions.Length > 0)
        {
            // Set the current target position as the agent's destination
            agent.destination = targetPositions[currentTargetIndex].position;

            // Check if the agent has reached the current target position
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // Move to the next target position
                currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
            }
        }
    }
}

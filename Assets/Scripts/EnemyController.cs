using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform wayPoints;
    private int currentWaypoint = 0;

    [Header("Components")]
    NavMeshAgent agent;

    bool inVisionCone = false;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPoints.GetChild(currentWaypoint).position); //Set destination to next waypoint at the start, so our first waypoint is the first child of our waypoints
    }

    private void Update()
    {
        
        if(agent.remainingDistance <= 0.2f) //If we get to a new waypoint, run below
        {
            currentWaypoint++; //Update to reflect moving to new waypoint
            if(currentWaypoint >= wayPoints.childCount) //If we have traveled back to all waypoints, reset currentWaypoint to 0
            {
                currentWaypoint = 0;
            }

            agent.SetDestination(wayPoints.GetChild(currentWaypoint).position); //Set destination to next waypoint
        }
    }
}

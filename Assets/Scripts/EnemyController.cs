using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform wayPoints;
    private int currentWaypoint;

    [Header("Components")]
    NavMeshAgent agent;

    bool inVisionCone = false;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
        if(agent.remainingDistance <= 0.2f)
        {
            currentWaypoint++;
            if(currentWaypoint >= wayPoints.childCount)
            {
                currentWaypoint = 0;
            }

            agent.SetDestination(wayPoints.GetChild(currentWaypoint).position);
        }
    }
}

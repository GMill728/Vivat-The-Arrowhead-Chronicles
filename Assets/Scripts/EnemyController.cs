using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    FieldOfView fieldOfView;
    [SerializeField] GameObject fieldOfViewScript;

    void Awake()
    {
        fieldOfView = fieldOfViewScript.GetComponent<FieldOfView>();
    }

    [Header("Patrol")]
    [SerializeField] private Transform wayPoints;
    private int currentWaypoint = 0;

    [Header("Components")]
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPoints.GetChild(currentWaypoint).position); //Set destination to next waypoint at the start, so our first waypoint is the first child of our waypoints
    }

    private void Update()
    {
        agent.isStopped = false;
        if(fieldOfView.playerinFOV == false){
            if(agent.remainingDistance <= 0.2f) //If we get to a new waypoint, run below
            {
                currentWaypoint++; //Update to reflect moving to new waypoint
                if(currentWaypoint >= wayPoints.childCount) //If we have traveled back to all waypoints, reset currentWaypoint to 0
                {
                    
                    currentWaypoint = 0;
                }

                agent.SetDestination(wayPoints.GetChild(currentWaypoint).position); //Set destination to next waypoint
            }
        } else 
        {
            //Debug.Log("Player detected! Stopping movement.");
            agent.isStopped = true;

            //Added by Luke - trigger guard dialogue when player is caught
            string guardName = GameObject.FindWithTag("Guard").GetComponent<NpcDialogueActor>().ActorName;  //retrive actor name
            string guardDialogueNum = GameObject.FindWithTag("Guard").GetComponent<NpcDialogueActor>().interactDialogueNum; // retrieve actor starting dialogue
            DialogueManager.Instance.linkActorVar = guardName;  //update DialogueManager's temp variables for circumstances requiring these strings
            DialogueManager.Instance.linkNodeIdVar = guardDialogueNum;
            DialogueManager.Instance.SpeakToNewActor(guardName, guardDialogueNum); //Begin dialogue
        }
    }
}

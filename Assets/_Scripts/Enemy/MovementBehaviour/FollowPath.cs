using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MovementBehaviour
{


    [SerializeField] private Waypoints waypoints;

    private Transform currentWaypoint;
    float currentWaitTime;
    [SerializeField] private float waitTimeBeforeMovingAgain;
    Enemy enemy;

    [Tooltip("This is what determines how close the enemy should get to their waypoint before trying to move to the next one")]
    [Range(0.1f, 1f)]
    [SerializeField] private float distanceThreshold;
    [SerializeField] private bool waitAtLastWaypoint;
    [SerializeField] bool isLooping;
    [SerializeField] bool isAlternating;
    [SerializeField] bool canMove;
    
    bool reverseDirection;

    void Awake()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }

    public override void RunMovementBehaviour(GameObject parent)
    {
        enemy = GetComponent<Enemy>();
        FollowWaypoint();
    }

    public void FollowWaypoint()
    {
        enemy.rb.MovePosition(Vector3.MoveTowards(enemy.rb.position, currentWaypoint.position, enemy.movementSpeed * Time.fixedDeltaTime));
        if(Vector3.Distance(enemy.rb.position, currentWaypoint.position) <= 0.1)
        {   
               // If the current waypoint is the first in it's line or the last in it's line, wait some time before moving again.
            if(currentWaypoint.GetSiblingIndex() == waypoints.transform.childCount -1 || currentWaypoint.GetSiblingIndex() == 0)  
            {
                //If this is the last waypoint in the list, determine what to do
                if(waitAtLastWaypoint)
                {
                    currentWaitTime = waitTimeBeforeMovingAgain;
                }

                if(isAlternating)
                {
                    reverseDirection = !reverseDirection;
                }
            }
            if(isAlternating && reverseDirection)
            {
                //If the enemy is alternating and the direction is reversed, get previous waypoints
                currentWaypoint = waypoints.GetPreviousWaypoint(currentWaypoint);
            }
            else
            {
                //Follow the waypoints as normal
                currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            }

        }
    }
}

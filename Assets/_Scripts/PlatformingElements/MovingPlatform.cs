using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float platformMoveSpeed;

    [SerializeField] private float waitTimeBeforeMovingAgain;
    float currentWaitTime;
    
    private Rigidbody rb;
    private Transform currentWaypoint;

    //Assigned a list of waypoints
    //Toggles needed:
    // Follow the loop = Standard function
    // Back and forth = need to go in a reverse loop of the waypoints
    // Wait times = tie that to the waypoints themselves
    [Header("Platform Properties")]
    [Description("Enable the platform to move")]
    [SerializeField] bool isActive;
    [Description("Enable if the platform should wait at it's last stops")]
    [SerializeField] private bool waitAtLastWaypoint;
    [SerializeField] bool isLooping;
    [SerializeField] bool isAlternating;
    [SerializeField] bool canMove;

    bool isMoving;

    bool reverseDirection;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        rb.position = currentWaypoint.position;
        //Define the starting position of the platform on its route
    }

    void Update()
    {
        isMoving = GetComponent<VelocityCalculator>().GetVelocity() != Vector3.zero;

        if(!isActive)
        {
            return;
        }

        if(currentWaitTime > 0)
        {
            canMove = false;
            currentWaitTime -= Time.deltaTime;
        }
        else
        {
            canMove = true;
        }


    }
    void FixedUpdate()
    {
        if(canMove)
        {
            FollowWaypoint();
        }
    }

    void FollowWaypoint()
    {
        rb.position = Vector3.MoveTowards(rb.position, currentWaypoint.position, platformMoveSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(rb.position, currentWaypoint.position) <= 0.1)
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
                //If the platform is alternating and the direction is reversed, get previous waypoints
                currentWaypoint = waypoints.GetPreviousWaypoint(currentWaypoint);
            }
            else
            {
                //Follow the waypoints as normal
                currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {

            Vector3 platformVelocity = GetComponent<VelocityCalculator>().GetVelocity();

            player.character.Move(platformVelocity * Time.fixedDeltaTime);
            


            //player.moveDirection = GetComponent<VelocityCalculator>().GetVelocity();

            
            //player.transform.SetParent(transform);
            //player.velocity = GetComponent<VelocityCalculator>().GetVelocity();
        }
    }



}

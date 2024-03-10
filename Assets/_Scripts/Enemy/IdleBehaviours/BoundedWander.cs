using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedWander : IdleBehaviour
{

    //This is the behaviour for a bounded wander
    //Basically, there must be a SOME component or transform that anchors the enemy and helps define the radius of
    //where the enemy can go

    //This behaviour specifically moves the enemy to a given position it decides when the wander is running
    //It is essentially like waypoints, except the list of points is essentially infinitely long and doesn't stick around

    //How the wander works
    //1. Pick a point within the radius of the point (or enemy)
    //2. Move towards that point
    //3. Continue untill there is no where left to go

    //Constraints:
    /*
        The wander point must always have the same Y level as the enemy.
        The enemy only ever moves forward at speed.
        
    */

  
    [SerializeField] float wanderRadius;
    private Transform currentWanderPoint;
    [SerializeField] float timeBetweenWander;
    public override void RunIdleBehaviour(GameObject parent)
    {
        Rigidbody rb = parent.GetComponent<Rigidbody>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndWander : MovementBehaviour
{

    [SerializeField] int wanderRadius;
    [SerializeField] int seekRadius;
    [SerializeField] LayerMask whatIsSolid;
    
    [SerializeField] LayerMask playerLayer;

        EnemyBehaviour behaviour;

    Vector3 currentWaypoint;

    Vector3 anchorPoint;
 
    //Have a raycast that always points down and always measures how close the machine is to the ground.
    //They cannot cross a Y value = the ground Y value + 1
    // Their seek is a 360 sphere

    //They have a maximum and minimum wander based on their anchor point
    //The anchor point is their position when they spawn in

    [SerializeField] float yThreshold;
    float yBoundary;


    void Awake()
    {
        behaviour = GetComponent<EnemyBehaviour>();
        //Set the anchor point to be the enemy's position when they wake up
        anchorPoint = transform.position;
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, whatIsSolid))
        {
            //Send out a Raycast. The Y of the contact point is the lower bound
            //We add the Threshold to it, so the floating enemy NEVER goes beyound that value
            yBoundary = hit.point.y + yThreshold;
        }
        
    }



    public override void RunMovementBehaviour(GameObject parent)
    {
        
        if(currentWaypoint == Vector3.zero)
        {
            currentWaypoint = behaviour.hostEnemy.rb.position;
        }

        if(currentWaypoint.y <= yBoundary)
        {
            currentWaypoint = GenerateRandomPoint();
        }

            behaviour.hostEnemy.transform.LookAt(currentWaypoint);
            MoveEnemyToPoint(behaviour.hostEnemy);


        if(Physics.CheckSphere(transform.position, seekRadius, playerLayer))
        {
            //The player is within range of the enemy
            //Debug.Log("Player is within range to draw aggro");
            behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Attack);
        }
    }

    Vector3 GenerateRandomPoint()
    {

        var newPoint = Vector3.zero;

        //Get the new point based on the anchor point
        //This will be recalled IF the Y value is LESS than the ground threshold
        newPoint = anchorPoint + new Vector3(Random.Range(-wanderRadius, wanderRadius), Random.Range(-wanderRadius, wanderRadius), Random.Range(-wanderRadius, wanderRadius));
        return newPoint;
    }

    void MoveEnemyToPoint(Enemy enemy)
    {
        enemy.rb.position = Vector3.MoveTowards(enemy.rb.position, currentWaypoint,  enemy.movementSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(enemy.rb.position, currentWaypoint) <= .8)
        {
            
            currentWaypoint = GenerateRandomPoint();
            //Debug.Log("Going to new point " + currentWaypoint);
        }
    }

    void OnDrawGizmos()
    {
        if(behaviour != null)
        Debug.DrawLine(behaviour.hostEnemy.rb.position + Vector3.up, currentWaypoint + Vector3.up, Color.red);
    }
}

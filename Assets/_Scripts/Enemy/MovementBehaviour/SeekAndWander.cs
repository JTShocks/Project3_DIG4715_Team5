using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndWander : MovementBehaviour
{


    [SerializeField] int wanderRadius;
    [SerializeField] int seekRadius;
    [SerializeField] LayerMask whatIsSolid;
    [SerializeField] LayerMask playerLayer;

    EnemyBehaviour behaviour;

    Vector3 currentWaypoint;
    //The enemy will wander in small random directions and look for the player
    // Start is called before the first frame update
    public override void RunMovementBehaviour(GameObject parent)
    {

        behaviour = parent.GetComponent<EnemyBehaviour>();
        if(currentWaypoint == Vector3.zero)
        {
            currentWaypoint = behaviour.hostEnemy.rb.position;
        }
        if(Physics.Raycast(behaviour.hostEnemy.rb.position + Vector3.up, currentWaypoint + Vector3.up, wanderRadius+2, whatIsSolid))
        {
            //If this returns true, there is something in the way of the enemy
            //They will wait for a few moments, then find a new location to move too            
            currentWaypoint = GenerateRandomPoint();
        }

        else
        {

            //var rot = Quaternion.LookRotation(currentWaypoint, Vector3.up);
            //behaviour.hostEnemy.rb.rotation = Quaternion.RotateTowards(behaviour.hostEnemy.rb.rotation, rot, 360 * Time.fixedDeltaTime);
            
            behaviour.hostEnemy.transform.LookAt(currentWaypoint);
            MoveEnemyToPoint(behaviour.hostEnemy);
        
        }


        if(Physics.CheckSphere(transform.position, seekRadius, playerLayer))
        {
            //The player is within range of the enemy
            Debug.Log("Player is within range to draw aggro");
            behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Attack);
        }

    }

    Vector3 GenerateRandomPoint()
    {

        var newPoint = Vector3.zero;

        newPoint = transform.position + new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    Enemy hostEnemy;

    //Store a reference to the behaviours of this enemy
    //When the state switches, the behaviour becomes active 
    private MovementBehaviour movementBehaviour;
    private AttackBehaviour attackBehaviour;
    private IdleBehaviour idleBehaviour;


    void Awake()
    {
        //Get the behaviour components
        hostEnemy = GetComponent<Enemy>();
        idleBehaviour = GetComponent<IdleBehaviour>();
        movementBehaviour = GetComponent<MovementBehaviour>();
        attackBehaviour = GetComponent<AttackBehaviour>();
    }


    // Update is called once per frame
    void Update()
    {
        switch(hostEnemy.state){
            case Enemy.EnemyState.Idle:
            idleBehaviour.RunIdleBehaviour(gameObject);
            break;

            case Enemy.EnemyState.Attack:
            attackBehaviour.RunAttackBehaviour(gameObject);
            break;
        }
    }

    void FixedUpdate()
    {
        if(hostEnemy.state == Enemy.EnemyState.Move)
        {
            movementBehaviour.RunMovementBehaviour(gameObject);
        }
    }


}

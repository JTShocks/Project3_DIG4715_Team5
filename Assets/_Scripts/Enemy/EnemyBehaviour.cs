using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public enum EnemyState{
        Idle,
        Move,
        Attack
    }

    public EnemyState state;
    internal Enemy hostEnemy;

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
        switch(state){
            case EnemyState.Idle:
            idleBehaviour.RunIdleBehaviour(gameObject);
            break;

            case EnemyState.Attack:
            attackBehaviour.RunAttackBehaviour(gameObject);
            break;
        }
    }

    void FixedUpdate()
    {
        if(state == EnemyState.Move)
        {
            movementBehaviour.RunMovementBehaviour(gameObject);
        }
    }

        public void ChangeEnemyState(EnemyState newState)
    {
        state = newState;
        switch(state){
            case EnemyState.Idle:

            break;
            case EnemyState.Move:

            break;
            case EnemyState.Attack:

            break;
        }
    }


}

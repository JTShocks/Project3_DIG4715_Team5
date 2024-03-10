using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{

    public enum EnemyState{
        Idle,
        Move,
        Attack
    }

    public EnemyState state;
    //Get a reference to the enemy behaviour
    EnemyBehaviour enemyBehaviour;

    internal Rigidbody rb;
    internal float movementSpeed;
    //Enemy needs
    //Health, Movespeed, Idle, Move, Attack states and behaviours

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
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

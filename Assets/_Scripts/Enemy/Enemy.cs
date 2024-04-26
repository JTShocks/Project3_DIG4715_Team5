using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{


    //Get a reference to the enemy behaviour
    EnemyBehaviour enemyBehaviour;
    
    [SerializeField] float currentHealth;

    internal Rigidbody rb;
    [SerializeField] internal float movementSpeed;
    [SerializeField] internal float damage;

    public event Action OnEnemyKilled;
    //Enemy needs
    //Health, Movespeed, Idle, Move, Attack states and behaviours

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    public virtual void KillEnemy()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }

    

}

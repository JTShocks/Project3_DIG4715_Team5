using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Callbacks;
using UnityEngine;

public class Charge : AttackBehaviour
{

    [SerializeField] float chargeUpTime;
    [Tooltip("This is to prevent the enemy from getting stuck in an orbit around a player should they either not run into a wall or hit the player ")]
    [SerializeField] float maxTimeAllowedCharging;
    [SerializeField] float turningRadius;
    [SerializeField] float chargeSpeed;

    bool isChargingUp;
    bool chargingAtPlayer;
    float currentChargeUpTime;
    float currentChargeDuration;

    Vector3 targetPos;
    EnemyBehaviour behaviour;

    void Awake()
    {
        behaviour = GetComponent<EnemyBehaviour>();
    }

    public override void RunAttackBehaviour(GameObject parent)
    {

        Transform player = PlayerController.playerTransform;
      
        targetPos = new Vector3(player.position.x, gameObject.transform.position.y, player.position.z);

        if(!isChargingUp)
        {
            isChargingUp = true;
            currentChargeUpTime = chargeUpTime;
        }

        if(currentChargeUpTime > 0)
        {
            currentChargeUpTime -= Time.deltaTime;
            //Have the enemy face the player while they are charging up
        }

        if(currentChargeUpTime <= 0)
        {
            chargingAtPlayer = true;
            currentChargeDuration += Time.deltaTime;
        }
        
        if(currentChargeDuration >= maxTimeAllowedCharging)
        {

            chargingAtPlayer = false;
            isChargingUp = false;
            currentChargeUpTime = 0;
            currentChargeDuration = 0;
            behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Idle);
        }
        


        //Charge behaviour is in two phases
        // 1: Charging up the attack
        // 2: Running towards the player

        //Borrow the rotation from the player script to limit the turning radius of during the charge

        


    }

    void FixedUpdate()
    {

        
        var rot = Quaternion.LookRotation(targetPos - transform.position);
        behaviour.hostEnemy.rb.rotation = Quaternion.RotateTowards(behaviour.hostEnemy.rb.rotation, rot, turningRadius * Time.fixedDeltaTime);

        if(chargingAtPlayer)
        {
            ChargeAtThePlayer();
        }
    }


    void ChargeAtThePlayer()
    {
        //This is the method for making the enemy charge at the player once the player has aggro'd them
        behaviour.hostEnemy.rb.position += transform.forward * chargeSpeed * Time.fixedDeltaTime;

    }

    void OnCollisionEnter(Collision collision)
    {
        if(chargingAtPlayer)
        {
            //Deal damage to the player
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekAndDestroy : AttackBehaviour
{
   //The enemy will move towards the player and deal damage on contact

   
   EnemyBehaviour behaviour;
   [SerializeField] float aggroRange;

   float moveSpeedModifier = 1;

   Vector3 targetPos;

   void Awake()
   {
      behaviour = GetComponent<EnemyBehaviour>();
   }
   public override void RunAttackBehaviour(GameObject parent)
   {
      Transform player = PlayerController.playerTransform;
      //behaviour = parent.GetComponent<EnemyBehaviour>();
      targetPos = new Vector3(player.position.x, gameObject.transform.position.y, player.position.z);

      behaviour.hostEnemy.transform.LookAt(targetPos);
      MoveEnemyToPoint(behaviour.hostEnemy);

      if(Vector3.Distance(behaviour.hostEnemy.rb.position, targetPos) >= aggroRange)
      {
         behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Idle);
      }
      
   }

    void MoveEnemyToPoint(Enemy enemy)
    {
        enemy.rb.position = Vector3.MoveTowards(enemy.rb.position, targetPos,  enemy.movementSpeed * moveSpeedModifier* Time.fixedDeltaTime);
        if(Vector3.Distance(enemy.rb.position, targetPos) <= 0.75)
        {
            
        }   

    }

   void OnCollisionStay(Collision other)
   {
      if(other.gameObject.CompareTag("Player"))
      {
         moveSpeedModifier = 0;
         ITakeDamage target = other.collider.GetComponent<ITakeDamage>();
         if(target != null)
         {
            target.TakeDamage(behaviour.hostEnemy.damage);
            behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Idle);
         }
      }
      else
      {
         moveSpeedModifier = 1;
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAndDestroy : AttackBehaviour
{
   EnemyBehaviour behaviour;
   [SerializeField] float aggroRange;

   float moveSpeedModifier = 1;

   Vector3 targetPos;

   [SerializeField] float yThreshold;
       [SerializeField] LayerMask whatIsSolid;
   float yBoundary;

      void Awake()
   {
      behaviour = GetComponent<EnemyBehaviour>();

              RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, whatIsSolid))
        {
            //Send out a Raycast. The Y of the contact point is the lower bound
            //We add the Threshold to it, so the floating enemy NEVER goes beyound that value
            yBoundary = hit.point.y + yThreshold;
        }
   }
   public override void RunAttackBehaviour(GameObject parent)
   {
      Transform player = PlayerController.playerTransform;
      //behaviour = parent.GetComponent<EnemyBehaviour>();

      float targetYPos = player.position.y;
      if(targetYPos < yBoundary)
      {
        targetYPos = yBoundary;
      }
      targetPos = new Vector3(player.position.x, targetYPos, player.position.z);

      behaviour.hostEnemy.transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
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

         ITakeDamage target = other.collider.GetComponent<ITakeDamage>();
         if(target != null)
         {
            target.TakeDamage(behaviour.hostEnemy.damage);
            behaviour.ChangeEnemyState(EnemyBehaviour.EnemyState.Idle);
         }
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekAndDestroy : AttackBehaviour
{
   //The enemy will move towards the player and deal damage on contact

   
   EnemyBehaviour behaviour;
   [SerializeField] float aggroRange;

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
        enemy.rb.position = Vector3.MoveTowards(enemy.rb.position, targetPos,  enemy.movementSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(enemy.rb.position, targetPos) <= 0.5)
        {
            Debug.Log("Should be dealing contact damage");
        }
    }
}

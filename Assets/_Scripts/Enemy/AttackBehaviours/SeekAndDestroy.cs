using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : AttackBehaviour
{
   //The enemy will move towards the player and deal damage on contact


   public override void RunAttackBehaviour(GameObject parent)
   {
        Debug.Log("Enemy is in attack mode!");
   }
}

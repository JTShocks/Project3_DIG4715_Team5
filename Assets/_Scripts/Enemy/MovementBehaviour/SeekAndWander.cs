using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndWander : MovementBehaviour
{

    
    //The enemy will wander in small random directions and look for the player
    // Start is called before the first frame update
    public override void RunMovementBehaviour(GameObject parent)
    {
        
        EnemyBehaviour enemy = parent.GetComponent<EnemyBehaviour>();
    }
}

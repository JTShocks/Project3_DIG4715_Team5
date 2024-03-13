using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StandIdle : IdleBehaviour
{

    EnemyBehaviour hostEnemy;

    bool isRunning;
    [Header("Behaviour values")]
    [SerializeField] private float timeToStandIdle;
    [Description("How long you want the enemy to stand idle when they get into this state. Set to -1 to make them stand idle forever.")]
    float currentTime;
    private Coroutine idleCoroutine;



    void Awake()
    {
        hostEnemy = GetComponent<EnemyBehaviour>();
    }

    public override void RunIdleBehaviour(GameObject parent)
    {
        if(!isRunning)
        {
            isRunning = true;
            if(idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
            }
            StartCoroutine(Idling());
        }
    }

    IEnumerator Idling()
    {
        yield return new WaitForSeconds(timeToStandIdle);
        hostEnemy.ChangeEnemyState(EnemyBehaviour.EnemyState.Move);
        isRunning = false;
        yield return null;
    }
    
}

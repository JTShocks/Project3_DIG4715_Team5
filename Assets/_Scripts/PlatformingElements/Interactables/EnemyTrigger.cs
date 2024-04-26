using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] Enemy enemyTrigger;
    // Start is called before the first frame update

    [SerializeField] UnityEvent OnActivateThis;

    void OnEnable(){
        if(enemyTrigger != null)
        {
            enemyTrigger.OnEnemyKilled += ActivateThis;
        }

    }
    void OnDisable(){
        if(enemyTrigger != null)
        {
            enemyTrigger.OnEnemyKilled -= ActivateThis;
        }

    }
    void ActivateThis()
    {
        OnActivateThis?.Invoke();
    }
}

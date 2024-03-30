using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{

    public static Action OnPlayerChangeHealth;


    [SerializeField] private int invulnerableTime;
    public bool isInvulnerable;
    float currentInvulnerableTime;

    Coroutine invincibleTimer;

    //These are static variables since there is only going to be 1 of these scripts and the health should persist between the scenes
        public static int MaxHealth = 5;
    public static int CurrentHealth {get; private set;}


    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    IEnumerator InvincibleTime()
    {
        isInvulnerable = true;
        Debug.Log("Player is invincible");
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
        Debug.Log("Player is not longer invincible");
        yield return null;
    }

    public void TakeDamage(float amount)
    {
        if(isInvulnerable)
        {
            return;
        }
        int damage = (int)amount;
        //When the player takes damage, they are invulnerable for a period of time after taking damage
        ChangeHealth(-damage);
        if(invincibleTimer != null)
        {
            StopCoroutine(InvincibleTime());
        }
        invincibleTimer = StartCoroutine(InvincibleTime());


    }

    public void HealDamage(int amount)
    {
        ChangeHealth(amount);
    }

    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
        //Always + the amount and just make take damage insert a negative value
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnPlayerChangeHealth?.Invoke();
        //Invoke an event when the player health is changed

        Debug.Log(CurrentHealth);

        if(CurrentHealth == 0)
        {
            //Signal that the player has died
            Die();
        }
    }

    void Die()
    {
        //What to do when the player reaches 0 health
        //This would be the respawning
    }


}

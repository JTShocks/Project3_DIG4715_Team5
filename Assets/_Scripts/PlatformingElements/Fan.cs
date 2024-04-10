using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    CapsuleCollider capCollider;
    [SerializeField] internal bool isActive;
    [SerializeField] int fanHeight;
    [SerializeField] float fanElevateSpeed;


    void Awake()
    {
        capCollider = GetComponent<CapsuleCollider>();
        CalculateColliderBounds();
        
    }

    void CalculateColliderBounds()
    {
        capCollider.height = fanHeight;

        float heightToCenterDiff = (fanHeight-5)/5 ;

        Vector3 capCenter = new Vector3(0,1 + (heightToCenterDiff * 2.5f), 0);
        capCollider.center = capCenter;
    }

    void OnTriggerStay(Collider collider)
    {

        if(!isActive)
        {
            return;
        }
        PlayerController player = collider.GetComponentInParent<PlayerController>();

        if(player != null)
        {
            GlideAbilityHolder holder = collider.GetComponent<GlideAbilityHolder>();
            if(holder.state == GlideAbilityHolder.AbilityState.Active)
            {
                player.velocity.y = fanElevateSpeed;
            }
        }
    }
}

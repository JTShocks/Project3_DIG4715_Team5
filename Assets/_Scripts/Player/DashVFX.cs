using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashVFX : MonoBehaviour
{

    [SerializeField] ParticleSystem dashParticle;
    public void PlayDashVFX()
    {
        dashParticle.Play();
    }
}

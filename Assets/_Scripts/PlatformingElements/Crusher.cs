using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    // Can be changed in the inspector if needed.
    public float swingInterval = 2f;
    public float swingDuration = 1f;
    public int damage = 5;
    public float targetRotationX = 0f;
    public float targetRotationY = 0f;
    public float targetRotationZ = 135f;
    // Swing thing.
    private float nextSwingTime = 0f;
    private bool isSwingingDown = true;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float swingTime;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(targetRotationX, targetRotationY, targetRotationZ);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSwingTime)
        {
            if(swingTime < swingDuration)
            {
                swingTime += Time.deltaTime;
                float lerpFactor = swingTime / swingDuration;
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerpFactor);
            }
            else
            {
                swingTime = 0;
                nextSwingTime = Time.time + swingInterval;

                Quaternion temp = startRotation;
                startRotation = endRotation;
                endRotation = temp;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}

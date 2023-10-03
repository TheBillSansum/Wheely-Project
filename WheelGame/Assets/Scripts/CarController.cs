using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimpleCarController : MonoBehaviour
{

    public GameObject exhaustTextures;
    public ParticleSystem[] exhaust = new ParticleSystem[4];
    public float accelerationForce = 100f;   // The force to accelerate the car.
    public float steeringForce = 1000f;        // The force to steer the car.
    public float maxSpeed = 10f;            // The maximum speed of the car.
    public float slipperyFactor = 0.9f;     // A factor for slipperiness.

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.freezeRotation = true; // Freeze rotation to prevent the car from tipping over.
    }

    private void Update()
    {
        // Get input for acceleration and steering.
        float accelerationInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        // Calculate the forward force (acceleration).
        Vector3 forwardForce = transform.forward * accelerationInput * accelerationForce;

        // Apply the forward force while considering slipperiness.
        rb.AddForce(forwardForce * slipperyFactor);

        // Calculate the torque for steering.
        Vector3 torque = transform.up * steeringInput * steeringForce;

        // Apply the torque to steer the car.
        rb.AddTorque(torque);

        // Limit the car's speed.
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }





        if (Input.GetAxis("Vertical") != 0)
        {
            exhaust[0].Play();
            exhaust[1].Play();
            exhaust[2].Play();
            exhaust[3].Play();
        }
        else
        {
            exhaust[0].Pause();
            exhaust[1].Pause();
            exhaust[2].Pause();
            exhaust[3].Pause();
        }
    }
}



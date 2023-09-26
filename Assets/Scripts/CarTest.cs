using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour
{
    public WheelCollider[] wheelColliders;
    public Transform[] wheelTransforms;
    public float maxSteerAngle = 30f;
    public float maxMotorTorque = 1000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 50f;
    public float reverseSpeed;
    public Vector3 currentVelocity;

    public Rigidbody rb;

    public float currentSpeed;
    public float steeringInput;
    public float throttleInput;
    public float brakeInput;

    private void Start()
    {
        // Assign wheel transforms if not set
        if (wheelTransforms == null || wheelTransforms.Length == 0)
        {
            wheelTransforms = new Transform[wheelColliders.Length];
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelTransforms[i] = wheelColliders[i].transform;
            }
        }
    }

    private void Update()
    {

        steeringInput = Input.GetAxis("Horizontal");
        throttleInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

    

        currentSpeed = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).z, reverseSpeed, maxSpeed);
        rb.velocity = transform.TransformDirection(new Vector3(0, 0, currentSpeed));
 currentVelocity = rb.velocity;
       // if (currentSpeed > maxSpeed && currentSpeed >=0)
        //{


        //if (currentSpeed < reverseSpeed)
        //{
         //   rb.velocity = transform.TransformDirection(new Vector3(0, 0, reverseSpeed));
        //}
    
    }
    

    private void FixedUpdate()
    {
        // Steering
        float steerAngle = maxSteerAngle * steeringInput;

            wheelColliders[0].steerAngle = steerAngle;
            wheelColliders[1].steerAngle = steerAngle;
        

        // Throttle and braking
        float motorTorque = maxMotorTorque * throttleInput;
        foreach (var wheel in wheelColliders)
        {
            if (brakeInput > 0)
            {
                wheel.brakeTorque = brakeTorque * brakeInput;
                wheel.motorTorque = 0;
            }
            else
            {
                wheel.brakeTorque = 0;
                wheel.motorTorque = motorTorque;
            }
        }

        // Update wheel rotations for visuals
        UpdateWheelTransforms();
    }

    void UpdateWheelTransforms()
    {
        for (int i = 0; i < wheelTransforms.Length; i++)
        {
            Quaternion rot;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out rot);
            wheelTransforms[i].rotation = rot;
            wheelTransforms[i].position = pos;
        }
    }
}
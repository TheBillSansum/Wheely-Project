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
    public float rpmCap = 1000;


    public Rigidbody rb;


    public float currentSpeed;
    public float steeringInput;
    public float throttleInput;
    public float brakeInput;

    public float groundedHeight = 0.5f;
    public float checkRate = 1.0f; 
    public bool grounded = false;
    public LayerMask groundLayer;
    public float heightOffset = 0.25f; 


    public TrailRenderer[] wheelMarks;
    public GameObject[] breakLight;
    public Material breaklightMaterial;

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

        InvokeRepeating("GroundCheck", 0, checkRate);
    }

    private void Update()
    {

        steeringInput = Input.GetAxis("Horizontal");
        throttleInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        if(throttleInput > 0)
        {
            breakLight[0].SetActive(true);
            breakLight[1].SetActive(true);
            breaklightMaterial.SetFloat("_Metallic", 1);
           
        }
        else
        {
            breakLight[0].SetActive(false);
            breakLight[1].SetActive(false);
            breaklightMaterial.SetFloat("_Metallic", 0);
        }
    }
    

    private void FixedUpdate()
    {
        // Steering
        float steerAngle = maxSteerAngle * steeringInput;

            wheelColliders[0].steerAngle = steerAngle;
            wheelColliders[1].steerAngle = steerAngle;


                currentSpeed = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).z, maxSpeed, reverseSpeed);

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
                if (Mathf.Abs(wheel.rpm) > rpmCap)
                {
                    wheel.motorTorque = 0;
                }
                else
                {
                   wheel.motorTorque = motorTorque;
                }

            }
        }
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
    void GroundCheck()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))
        {
            grounded = true;
            updateWheelMarks();
        }
        else
        {
            grounded = false;
            updateWheelMarks();
        }
    }


    public void updateWheelMarks()
    {
        wheelMarks[0].emitting = grounded;
        wheelMarks[1].emitting = grounded;
        wheelMarks[2].emitting = grounded;
        wheelMarks[3].emitting = grounded;
    }
}
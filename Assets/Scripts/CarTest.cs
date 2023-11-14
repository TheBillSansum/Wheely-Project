using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour
{
    #region Variables

    public WheelCollider[] wheelColliders;
    public Transform[] wheelTransforms;
    public float maxSteerAngle = 30f;
    public float maxMotorTorque = 1000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 50f;
    public float reverseSpeed;
    public Vector3 currentVelocity;
    public float rpmCap = 1000;

    public AudioManager audioManager;


    public Rigidbody rb;
    public float flipStrength = 3f;


    public float currentSpeed;
    public float steeringInput;
    public float throttleInput;
    public float brakeInput;

    public float groundedHeight = 0.5f;
    public float checkRate = 1.0f;
    public bool grounded = false;
    public LayerMask groundLayer;
    public float heightOffset = 0.25f;
    public ConstantForce gravity;


    public TrailRenderer[] wheelMarks;
    public GameObject[] breakLight;
    public Material breaklightMaterial;
    public GameObject[] frontLights;
    public bool lightsOn = false;

    public GameObject[] wheelPrefab;

    #endregion

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

        gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0.0f, -2f, 0.0f);

        //InvokeRepeating("GroundCheck", 0, checkRate);
    }

    private void Update()
    {

        steeringInput = Input.GetAxis("Horizontal");
        throttleInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        if (throttleInput > 0)
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

        if (Input.GetKey(KeyCode.Q))
        {
            ResetCarLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            ResetCarRight();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            audioManager.PlayHint(2);
            
            if (lightsOn)
            {
                lightsOn = false;
                frontLights[0].SetActive(false);
                frontLights[1].SetActive(false);
            }
            else
            {
                lightsOn = true;
                frontLights[0].SetActive(true);
                frontLights[1].SetActive(true);
            }
        }
    }


    private void FixedUpdate()
    {
        float steerAngle = maxSteerAngle * steeringInput;

        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;


        currentSpeed = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).z, maxSpeed, reverseSpeed); // Not the issue ^

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

    /// <summary>
    /// PROBLEM CHILD :<
    /// </summary>
    void UpdateWheelTransforms()
    {
        for (int i = 0; i < wheelTransforms.Length; i++)
        {
            Quaternion rot;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out rot);
            wheelTransforms[i].rotation = rot;
            //wheelTransforms[i].position = pos;
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

    public void ResetCarLeft()
    {
        if (Mathf.Abs(transform.localRotation.eulerAngles.z) > 90f)
        {
            rb.AddRelativeTorque(0f, 0f, flipStrength, ForceMode.Acceleration);
        }
    }
    public void ResetCarRight()
    {
        if (Mathf.Abs(transform.localRotation.eulerAngles.z) > 90f)
        {
            rb.AddRelativeTorque(0f, 0f, -flipStrength, ForceMode.Acceleration);
        }
    }
}

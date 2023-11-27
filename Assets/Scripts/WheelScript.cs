using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{

    public bool grounded = false;
    public LayerMask groundLayer;
    public float heightOffset = -1f;
    public TrailRenderer wheelMarks; 
    public float groundedHeight = -0.1f;

    public CarTest carScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        Vector3 rayStart = new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z);
        Vector3 rayEnd = rayStart - Vector3.up * (groundedHeight + heightOffset);
        Debug.DrawLine(rayStart, rayEnd, Color.red);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))

        {
            grounded = true;
            wheelMarks.emitting = grounded;
            carScript.grounded = true;
        }
        else
        {
            grounded = false;
            wheelMarks.emitting = grounded;
            carScript.grounded = false;
        }
    }
}

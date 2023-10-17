using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{

    public bool grounded = false;
    public LayerMask groundLayer;
    public float heightOffset = 0.25f;
    public TrailRenderer wheelMarks; 
    public float groundedHeight = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))
        {
            grounded = true;
            wheelMarks.emitting = grounded;
        }
        else
        {
            grounded = false;
            wheelMarks.emitting = grounded;
        }
    }
}

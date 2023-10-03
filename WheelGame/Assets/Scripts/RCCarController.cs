using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCarController : MonoBehaviour
{
    public float speed = 5.0f;  // Speed of the car.
    public float rotationSpeed = 100.0f;  // Rotation speed of the car.

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get user input for movement and rotation.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement and rotation.
        Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Apply movement and rotation.
        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * rotation);
    }
}

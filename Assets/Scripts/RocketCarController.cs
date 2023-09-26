using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCarController : MonoBehaviour
{
    public float speed = 10f;        // Car driving speed.
    public float rotationSpeed = 1f; // Rotation speed.
    public float jumpForce = 5f;     // Jump force.
    public float boostForce = 20f;   // Boost force.

    private Rigidbody rb;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Car movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = moveDirection.normalized * speed;

        // Boost.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            velocity += transform.forward * boostForce;
        }

        rb.velocity = velocity;

        // Rotation.
        float rotationInput = Input.GetAxis("Rotate");
        Vector3 rotation = new Vector3(0, rotationInput * rotationSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SweepingDangerZone : MonoBehaviour
    {
    public Transform player;
    public Transform rayOrigin;
    public float rotationSpeed = 30.0f;
    public float rayLength = 10.0f;
    public float downwardAngle = 15.0f; // Angle at which the ray aims downward
    public LayerMask hitLayers; // Layers that the ray should hit

    private LineRenderer lineRenderer; // Used to visually represent the danger zone

    void Start()
    {
        // Initialize the LineRenderer component for visualizing the danger zone
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 2f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.red };
    
        }

    void Update()
    {
        // Rotate the ray origin
        rayOrigin.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Calculate the downward direction
        Vector3 forward = rayOrigin.forward;
        Vector3 downward = Quaternion.Euler(-downwardAngle, 0, 0) * Vector3.forward;
        Vector3 direction = (forward + downward).normalized;

        // Cast a ray from the ray origin
        RaycastHit hit;
        bool isHit = Physics.Raycast(rayOrigin.position, direction, out hit, rayLength, hitLayers);

        // Draw the ray in the scene
        lineRenderer.SetPosition(0, rayOrigin.position);
        lineRenderer.SetPosition(1, isHit ? hit.point : rayOrigin.position + direction * rayLength);

        // Check if the player is within the danger zone
        if (isHit && hit.transform == player)
        {
            Debug.Log("Player caught in the danger zone!");
            // Implement what happens when the player is caught
        }
    }
}
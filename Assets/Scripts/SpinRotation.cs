using UnityEngine;

/// <summary>
/// Makes an object spin continuously.
/// Attach to loot/pickup prefabs for a nice visual effect.
/// </summary>
public class SpinRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 50f; // Degrees per second
    public Vector3 rotationAxis = Vector3.up; // Y-axis by default

    [Header("Bob Effect (Optional)")]
    public bool enableBobbing = true;
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the object
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        // Optional bobbing effect (moves up and down)
        if (enableBobbing)
        {
            Vector3 newPosition = startPosition;
            newPosition.y += Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = newPosition;
        }
    }
}

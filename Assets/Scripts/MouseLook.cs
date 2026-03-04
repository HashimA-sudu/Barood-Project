using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        // Drag "CameraTarget" here
    public float distance = 3.0f;   // How far behind the player
    public float sensitivity = 10f;
    
    float rotationX = 0;
    float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() // Use LateUpdate for cameras to prevent jitter
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        rotationY += mouseDelta.x * sensitivity * Time.deltaTime;
        rotationX -= mouseDelta.y * sensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -20, 45); // Limit looking up/down

        // Calculate rotation and position
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.rotation = rotation;
        transform.position = position;
    }
}
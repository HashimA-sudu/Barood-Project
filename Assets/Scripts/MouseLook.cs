using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 10f;
    public Transform playerBody; 
    public float interactRange = 3f;
    
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition = new Vector3(0, 0.8f, 0); 
    }

    void Update()
    {
        // Prevent camera movement and interaction during conversations
        if (NPCUIManager.Instance != null && NPCUIManager.Instance.IsMenuBusy() == true)
        {
            return; 
        }
        // Camera Rotation
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        xRotation -= mouseDelta.y * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseDelta.x * sensitivity * Time.deltaTime);

        
        // Interact logic
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Inside your Update() function in FirstPersonCamera.cs
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
            {
                Debug.Log("I hit: " + hit.collider.name); // ADD THIS LINE
                if (hit.collider.TryGetComponent(out IInteractable obj))
                {
                    obj.Interact();
                }
            }
}
        }
    }
}
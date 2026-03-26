using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public float interactRange = 3f;
    public GameObject interactUI;

    public float sensitivity = 10f;
    public Transform playerBody;     
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition = new Vector3(0, 0.8f, 0); 
    }

    void Update()
    {
        bool isDialogueActive = NPCUIManager.Instance != null && NPCUIManager.Instance.IsMenuBusy() == true;
        bool isGamePaused = PauseMenu.Instance!=null && PauseMenu.Instance.isPaused;

        // Prevent camera movement and interaction during UI elements
        if (isDialogueActive || isGamePaused)
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
       Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            // Try to find the IInteractable interface on the gate or NPC
            if (hit.collider.TryGetComponent(out IInteractable obj))
            {
                // 1. Show the [E] Prompt box
                interactUI.SetActive(true); 
                
                // 2. Update the text (e.g., "Press [E] to Open Gate")
                    TMPro.TextMeshProUGUI promptText = interactUI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                    if(promptText != null) promptText.text = obj.GetInteractLabel();

                    // 3. Trigger the interaction
                    if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        obj.Interact();
                    }
                }
                else
                {
                    interactUI.SetActive(false); // Not looking at something interactive
                }
            }
            else
            {
                interactUI.SetActive(false); // Too far away
            }
            }
}
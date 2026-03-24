using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float slideSpeed = 12f;
    public float gravity = -9.81f;
    
    [Header("Crouch & Slide Settings")]
    public float crouchHeight = 1f;
    public float standingHeight = 2f;
    
    Vector3 velocity;
    bool isSprinting;
    bool isCrouching;

    void Update()
    {
        // 1. Inputs
        isSprinting = Keyboard.current.shiftKey.isPressed;
        isCrouching = Keyboard.current.leftCtrlKey.isPressed;

        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float z = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
        Vector3 moveInput = transform.right * x + transform.forward * z;

        float currentSpeed = walkSpeed;
        // make sure we don't allow movement if the NPC UI is busy
        if (NPCUIManager.Instance != null && NPCUIManager.Instance.IsMenuBusy() == true) return;

        // 2. Slide vs Crouch
        if (isCrouching)
        {
            controller.height = crouchHeight;
            // Slide if running and actually moving forward
            if (isSprinting && moveInput.magnitude > 0.1f) currentSpeed = slideSpeed;
            else currentSpeed = walkSpeed * 0.5f;
        }
        else
        {
            controller.height = standingHeight;
            currentSpeed = isSprinting ? runSpeed : walkSpeed;
        }

        // 3. Move
        controller.Move(moveInput * currentSpeed * Time.deltaTime);

        // 4. Jump & Gravity
        if (Keyboard.current.spaceKey.wasPressedThisFrame && controller.isGrounded)
            velocity.y = Mathf.Sqrt(2f * -gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(controller.isGrounded && velocity.y < 0) velocity.y = -2f;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float slideSpeed = 10f;
    public float gravity = -9.81f;
    
    [Header("Heights")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;
    public float slideHeight = 0.5f; // Lower than crouching
    
    [Header("Slide Settings")]
    public float slideDuration = 0.02f;
    public float slideDecline = 16f; // How fast it slows down

    Vector3 velocity;
    bool isSprinting = false;
    bool isCrouching = false;
    bool isSliding = false;
    float slideTimer;

    void Update()
    {
        // Master Switch: Stop movement if any controls are disabled
        if (ControlsManager.AreControlsDisabled()) return;

        HandleInput();
        MovePlayer();
    }

    void HandleInput()
    {
        // 1. Sprint Toggle
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            isSprinting = !isSprinting;
            if (isSprinting) isCrouching = false; // Stop crouching if we start running
        }

        // 2. Crouch/Slide Trigger
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            if (isSprinting && controller.isGrounded && !isSliding)
            {
                StartSlide();
            }
            else
            {
                isCrouching = !isCrouching;
                isSliding = false;
            }
        }
    }

    void StartSlide()
    {
        isSliding = true;
        isCrouching = false;
        slideTimer = slideDuration;
        controller.height = slideHeight;
    }

    void MovePlayer()
    {
        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float z = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
        Vector3 moveInput = transform.right * x + transform.forward * z;

        float currentSpeed;

        if (isSliding)
        {
            currentSpeed = slideSpeed;
            slideTimer -= Time.deltaTime;
            
            // Gradually slow down the slide
            slideSpeed -= slideDecline * Time.deltaTime;

            if (slideTimer <= 0 || slideSpeed <= walkSpeed)
            {
                isSliding = false;
                slideSpeed = 16f; // Reset for next slide
                isCrouching = false; // Transition into a crouch
            }
        }
        else if (isCrouching)
        {
            controller.height = crouchHeight;
            currentSpeed = walkSpeed * 0.5f;
            isSprinting = false;
        }
        else
        {
            controller.height = standingHeight;
            currentSpeed = isSprinting ? runSpeed : walkSpeed;
        }

        controller.Move(moveInput * currentSpeed * Time.deltaTime);

        // Gravity & Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * -gravity);
            isCrouching = false; // Stand up when jumping
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
    }
}
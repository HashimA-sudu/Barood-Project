using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam; // Drag your Main Camera here
    public float speed = 5f;
    public float gravity = -9.81f;
    
    Vector3 velocity;
    Vector2 moveInput;

    void Update()
    {
        // 1. Get WASD Input
        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float y = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
        moveInput = new Vector2(x, y);

        // 2. Calculate direction relative to Camera
        // We take the camera's forward/right and flatten them (remove Y) so we don't walk into the ground
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0.1f; // Prevent zeroing out forward vector which can cause issues when looking straight up/down
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // This is where 'move' is declared and calculated
        Vector3 move = camRight * moveInput.x + camForward * moveInput.y;

        // 3. Apply Movement
        controller.Move(move * speed * Time.deltaTime);

        // 4. Rotation Logic: Make player face where they are walking
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // 5. Gravity Logic
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
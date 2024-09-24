using UnityEngine;
using UnityEngine.InputSystem;

public class SimulateVRMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Input Settings")]
    public InputActionProperty moveInputAction;
    public InputActionProperty jumpInputAction;

    [Header("References")]
    public Transform cameraTransform;  // Reference to the camera's transform

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Reset vertical velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure the player sticks to the ground
        }

        // Get the movement input (Vector2) from InputAction
        Vector2 input = moveInputAction.action.ReadValue<Vector2>();

        // Calculate movement direction based on camera orientation
        Vector3 move = cameraTransform.right * input.x + cameraTransform.forward * input.y;
        move.y = 0; // Ensure no vertical movement

        // Apply movement
        characterController.Move(move * speed * Time.deltaTime);

        // Gravity control
        verticalVelocity += gravity * Time.deltaTime;
        velocity.y = verticalVelocity;

        // Jump control
        if (jumpInputAction.action.triggered && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity and vertical movement
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        moveInputAction.action.Enable();
        jumpInputAction.action.Enable();
    }

    private void OnDisable()
    {
        moveInputAction.action.Disable();
        jumpInputAction.action.Disable();
    }
}

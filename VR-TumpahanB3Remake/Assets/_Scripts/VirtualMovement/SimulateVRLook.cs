using UnityEngine;
using UnityEngine.InputSystem;

public class SimulateVRLook : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionProperty lookAction; // Use InputActionProperty for the new Input System
    
    [Header("Look Sensitivity")]
    public float sensitivity = 1.0f;  // Sensitivity multiplier

    [Header("Clamping")]
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private Vector2 lookInput;  // To store input from the new input system
    private float verticalRotation = 0f;

    void OnEnable()
    {
        // Enable the input action when the script is enabled
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        // Disable the input action when the script is disabled
        lookAction.action.Disable();
    }

    void Update()
    {
        // Read input from the InputActionProperty
        lookInput = lookAction.action.ReadValue<Vector2>();

        // Rotate in the local space of the camera
        LookAround();
    }

    void LookAround()
    {
        // Horizontal rotation (yaw)
        float horizontalRotation = lookInput.x * sensitivity;
        transform.Rotate(Vector3.up, horizontalRotation, Space.Self);  // Rotate around local Y-axis

        // Vertical rotation (pitch)
        verticalRotation -= lookInput.y * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f); // Clamping pitch
    }
}

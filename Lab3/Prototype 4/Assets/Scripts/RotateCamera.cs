using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 150f;
    private InputSystem_Actions controls;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        Debug.Log(controls.Player.Move);
    }

    private void Update()
    {
        // Read the Move action (which is a Vector2)
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        float horizontalInput = moveInput.x; // Right/left (A/D or arrow keys)

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
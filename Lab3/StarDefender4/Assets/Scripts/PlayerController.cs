
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float horizontalLimit = 7.5f;

    private float moveInput;

    void Update()
    {
        moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f;
            }

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.right * moveInput * moveSpeed * Time.fixedDeltaTime;

        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);

        transform.position = newPosition;
    }
}

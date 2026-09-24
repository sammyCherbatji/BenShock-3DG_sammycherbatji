using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float massLoss = 0.01f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1;

        Vector3 movement = new Vector3(input.x, 0f, input.y);

        if (input != Vector2.zero)
        {
            rb.AddForce(movement * moveSpeed);

            transform.localScale -= Vector3.one * massLoss;

            float minimumSize = 0.3f;

            if (transform.localScale.x < minimumSize)
            {
                transform.localScale = Vector3.one * minimumSize;
            }
        }
    }
}
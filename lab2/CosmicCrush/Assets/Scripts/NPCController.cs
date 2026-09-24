using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 1.5f;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveDirection = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed);
    }
}
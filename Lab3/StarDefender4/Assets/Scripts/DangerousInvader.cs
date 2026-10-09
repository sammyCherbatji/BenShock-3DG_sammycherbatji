
using UnityEngine;

public class DangerousInvader : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float horizontalLimit = 5f;
    public float verticalLimit = 4f;

    private Vector3 movementDirection;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        transform.position += movementDirection * moveSpeed * Time.deltaTime;

        Vector3 position = transform.position;

        if (position.x > horizontalLimit || position.x < -horizontalLimit)
        {
            movementDirection.x *= -1;
        }

        if (position.y > verticalLimit || position.y < 0f)
        {
            movementDirection.y *= -1;
        }

        transform.position = position;
    }

    void ChooseNewDirection()
    {
        movementDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.5f, 0.5f),
            0f
        ).normalized;
    }
}


using UnityEngine;

public class InvaderFormation : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float horizontalLimit = 6f;
    public float dropDistance = 0.5f;

    private int direction = 1;

    void Update()
    {
        transform.position +=
            Vector3.right * direction * moveSpeed * Time.deltaTime;

        if (transform.position.x >= horizontalLimit && direction > 0)
        {
            direction = -1;
            MoveDown();
        }
        else if (transform.position.x <= -horizontalLimit && direction < 0)
        {
            direction = 1;
            MoveDown();
        }
    }

    void MoveDown()
    {
        transform.position += Vector3.down * dropDistance;
    }
}

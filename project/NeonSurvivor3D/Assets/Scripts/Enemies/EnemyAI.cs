
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.1f)
        {
            direction.Normalize();

            transform.position +=
                direction * moveSpeed * Time.deltaTime;

            transform.rotation =
                Quaternion.LookRotation(direction);
        }
    }
}
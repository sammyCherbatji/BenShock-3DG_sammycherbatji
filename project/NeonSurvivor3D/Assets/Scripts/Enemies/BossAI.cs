
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float moveSpeed = 2f;

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
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
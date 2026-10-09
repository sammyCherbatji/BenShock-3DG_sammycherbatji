
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position +=
            Vector3.down * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.LoseLife();
            }

            Destroy(gameObject);
        }
    }
}

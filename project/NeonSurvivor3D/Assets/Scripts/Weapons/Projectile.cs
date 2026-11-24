
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    public float lifetime = 3f;
    public int damage = 1;

    public bool isBossProjectile = false;

    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBossProjectile)
        {
            PlayerHealth playerHealth =
                other.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }

            return;
        }

        EnemyHealth enemy =
            other.GetComponentInParent<EnemyHealth>();

        BossHealth boss =
            other.GetComponentInParent<BossHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
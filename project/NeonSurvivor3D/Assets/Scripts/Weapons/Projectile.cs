
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    public float lifetime = 3f;
    public int damage = 1;

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
        EnemyHealth enemy =
            other.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            Debug.Log("ENEMY HIT: " + other.name);

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Projectile hit something else: " + other.name);
        }
    }
}
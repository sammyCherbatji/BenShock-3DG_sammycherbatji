
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position +=
            Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        InvaderHealth invader =
            other.GetComponent<InvaderHealth>();

        if (invader != null)
        {
            invader.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

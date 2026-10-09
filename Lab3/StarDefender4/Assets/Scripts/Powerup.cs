
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float lifetime = 8f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position +=
            Vector3.down * fallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting =
                other.GetComponent<PlayerShooting>();

            if (shooting != null)
            {
                shooting.ActivatePowerup(10f);
            }

            Destroy(gameObject);
        }
    }
}

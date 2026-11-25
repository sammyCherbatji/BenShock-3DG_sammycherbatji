
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public int healthRestore = 30;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.RestoreHealth(healthRestore);

            Debug.Log("Health Power-Up Collected!");

            Destroy(gameObject);
        }
    }
}
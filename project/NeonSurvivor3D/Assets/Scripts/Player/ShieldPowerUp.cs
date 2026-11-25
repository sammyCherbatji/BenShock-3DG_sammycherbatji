using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player =
            other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.ActivateShield();

            Debug.Log("SHIELD POWER-UP COLLECTED!");

            Destroy(gameObject);
        }
    }
}
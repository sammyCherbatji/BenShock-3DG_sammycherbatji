
using UnityEngine;

public class RapidFirePowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerShooting player =
            other.GetComponentInParent<PlayerShooting>();

        if (player != null)
        {
            player.ActivateRapidFire();

            Debug.Log("Rapid Fire Power-Up Collected!");

            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform player;

    public float fireRate = 2f;
    public float projectileSpeed = 12f;

    private float nextFireTime;

    private void Update()
    {
        if (player == null || projectilePrefab == null)
            return;

        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ShootAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        Vector3 spawnPosition = transform.position
            + direction * 2f;

        spawnPosition.y = transform.position.y;

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,
            Quaternion.identity
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.speed = projectileSpeed;
            projectileScript.isBossProjectile = true;
            projectileScript.SetDirection(direction);
        }
    }
}
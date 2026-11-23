
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public float fireRate = 0.2f;
    public float projectileSpeed = 25f;

    [Header("References")]
    public Camera mainCamera;

    private float nextFireTime;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        Plane groundPlane = new Plane(
            Vector3.up,
            Vector3.zero
        );

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.01f)
                return;

            direction.Normalize();

            Vector3 spawnPosition =
                transform.position + direction * 1.2f;

            GameObject bullet = Instantiate(
                projectilePrefab,
                spawnPosition,
                Quaternion.identity
            );

            Projectile projectile =
                bullet.GetComponent<Projectile>();

            projectile.speed = projectileSpeed;
            projectile.SetDirection(direction);
        }
    }
}
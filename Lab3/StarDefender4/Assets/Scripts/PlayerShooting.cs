
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;

    private float nextFireTime;
    private bool isPoweredUp = false;
    private Coroutine powerupCoroutine;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.spaceKey.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        if (isPoweredUp)
        {
            // Fire three bullets in a spread.
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Instantiate(
                bulletPrefab,
                firePoint.position + new Vector3(-0.3f, 0f, 0f),
                Quaternion.identity
            );

            Instantiate(
                bulletPrefab,
                firePoint.position + new Vector3(0.3f, 0f, 0f),
                Quaternion.identity
            );
        }
        else
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    public void ActivatePowerup(float duration)
    {
        if (powerupCoroutine != null)
            StopCoroutine(powerupCoroutine);

        powerupCoroutine = StartCoroutine(PowerupTimer(duration));
    }

    IEnumerator PowerupTimer(float duration)
    {
        isPoweredUp = true;
        Debug.Log("Weapon upgraded!");

        yield return new WaitForSeconds(duration);

        isPoweredUp = false;
        powerupCoroutine = null;

        Debug.Log("Weapon upgrade ended.");
    }
}


using UnityEngine;

public class DangerousInvaderShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float minimumShootDelay = 2f;
    public float maximumShootDelay = 4f;

    private float nextShootTime;

    void Start()
    {
        ScheduleNextShot();
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            ScheduleNextShot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null)
            return;

        Vector3 direction = new Vector3(
            Random.Range(-0.7f, 0.7f),
            -1f,
            0f
        ).normalized;

        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        AngledProjectile angledProjectile =
            projectile.GetComponent<AngledProjectile>();

        if (angledProjectile != null)
        {
            angledProjectile.SetDirection(direction);
        }
    }

    void ScheduleNextShot()
    {
        nextShootTime = Time.time +
            Random.Range(minimumShootDelay, maximumShootDelay);
    }
}

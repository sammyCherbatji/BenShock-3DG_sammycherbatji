
using UnityEngine;

public class InvaderShooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float minimumShootDelay = 1.5f;
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
        if (enemyBulletPrefab == null)
            return;

        Instantiate(
            enemyBulletPrefab,
            transform.position,
            Quaternion.identity
        );
    }

    void ScheduleNextShot()
    {
        nextShootTime = Time.time +
            Random.Range(minimumShootDelay, maximumShootDelay);
    }
}

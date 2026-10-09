
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;

    public float minimumSpawnDelay = 8f;
    public float maximumSpawnDelay = 15f;

    public float horizontalLimit = 6f;
    public float spawnHeight = 6f;

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerup();
            ScheduleNextSpawn();
        }
    }

    void SpawnPowerup()
    {
        if (powerupPrefab == null)
            return;

        float randomX = Random.Range(
            -horizontalLimit,
            horizontalLimit
        );

        Vector3 spawnPosition = new Vector3(
            randomX,
            spawnHeight,
            0f
        );

        Instantiate(
            powerupPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time +
            Random.Range(minimumSpawnDelay, maximumSpawnDelay);
    }
}

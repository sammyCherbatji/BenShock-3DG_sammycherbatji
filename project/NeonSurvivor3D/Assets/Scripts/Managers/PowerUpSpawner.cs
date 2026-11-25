using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Power-Up Prefabs")]
    public GameObject[] powerUpPrefabs;

    [Header("Spawn Settings")]
    public float spawnInterval = 15f;
    public int maxPowerUps = 3;

    [Header("Arena Boundaries")]
    public float minX = -18f;
    public float maxX = 18f;
    public float minZ = -18f;
    public float maxZ = 18f;

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerUp();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnPowerUp()
    {
        GameObject[] existingPowerUps =
            GameObject.FindGameObjectsWithTag("PowerUp");

        if (existingPowerUps.Length >= maxPowerUps)
            return;

        if (powerUpPrefabs.Length == 0)
            return;

        int randomIndex = Random.Range(0, powerUpPrefabs.Length);

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX),
            0.8f,
            Random.Range(minZ, maxZ)
        );

        Instantiate(
            powerUpPrefabs[randomIndex],
            spawnPosition,
            Quaternion.identity
        );
    }
}
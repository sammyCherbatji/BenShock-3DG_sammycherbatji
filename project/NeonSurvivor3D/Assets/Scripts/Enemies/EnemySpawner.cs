
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnDistance = 18f;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = player.position +
            new Vector3(
                randomDirection.x * spawnDistance,
                0f,
                randomDirection.y * spawnDistance
            );

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        enemy.tag = "Enemy";
    }
}
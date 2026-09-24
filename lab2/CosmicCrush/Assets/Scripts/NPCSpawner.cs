using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    public int numberOfNPCs = 15;

    void Start()
    {
        for (int i = 0; i < numberOfNPCs; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-8f, 8f),
                1f,
                Random.Range(-8f, 8f)
            );

            Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int numberOfNPCs = 15;

    void Start()
    {
        for (int i = 0; i < numberOfNPCs; i++)
        {
            Vector3 spawnPosition;

            do
            {
                spawnPosition = new Vector3(
                    Random.Range(-8f, 8f),
                    1f,
                    Random.Range(-8f, 8f)
                );
            }
            while (spawnPosition.magnitude < 3f);

            GameObject npc = Instantiate(
                npcPrefab,
                spawnPosition,
                Quaternion.identity
            );

            float size = Random.Range(0.5f, 1.6f);

            npc.transform.localScale = new Vector3(size, size, size);
        }
    }
}
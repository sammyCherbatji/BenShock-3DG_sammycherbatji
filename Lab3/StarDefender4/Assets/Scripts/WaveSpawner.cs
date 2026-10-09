
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject invaderFormationPrefab;
    public float spawnHeight = 3f;
    public float respawnDelay = 2f;

    private GameObject currentFormation;
    private bool waitingToRespawn = false;

    void Start()
    {
        InvaderFormation[] formations =
            FindObjectsOfType<InvaderFormation>();

        if (formations.Length > 0)
        {
            currentFormation = formations[0].gameObject;
        }
        else
        {
            SpawnFormation();
        }
    }

    void Update()
    {
        if (currentFormation != null &&
            currentFormation.transform.childCount == 0)
        {
            Destroy(currentFormation);
            currentFormation = null;
        }

        if (currentFormation == null && !waitingToRespawn)
        {
            StartCoroutine(RespawnFormation());
        }
    }

    IEnumerator RespawnFormation()
    {
        waitingToRespawn = true;

        yield return new WaitForSeconds(respawnDelay);

        SpawnFormation();

        waitingToRespawn = false;
    }

    void SpawnFormation()
    {
        if (invaderFormationPrefab == null)
        {
            Debug.LogError(
                "Assign the InvaderFormation prefab in the Inspector!"
            );
            return;
        }

        Vector3 spawnPosition =
            new Vector3(0f, spawnHeight, 0f);

        currentFormation = Instantiate(
            invaderFormationPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}

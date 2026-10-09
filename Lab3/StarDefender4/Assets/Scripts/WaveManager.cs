
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;

    public float delayBetweenWaves = 2f;

    private int currentWave = 1;
    private bool waitingForNextWave = false;

    public GameObject waveCompleteText;

    void Start()
    {
        wave1.SetActive(true);
        wave2.SetActive(false);
        wave3.SetActive(false);
    }

    void Update()
    {
        if (waitingForNextWave)
            return;

        if (currentWave == 1 && IsWaveCleared(wave1))
        {
            StartCoroutine(NextWave());
        }
        else if (currentWave == 2 && IsWaveCleared(wave2))
        {
            StartCoroutine(NextWave());
        }
        else if (currentWave == 3 && IsWaveCleared(wave3))
        {
            Debug.Log("All three waves completed!");

            if (waveCompleteText != null)
            {
                waveCompleteText.SetActive(true);
            }

            enabled = false;
        }
    }

    bool IsWaveCleared(GameObject wave)
    {
        if (wave == null)
            return true;

        if (currentWave == 1)
            return wave.transform.childCount == 0;

        return wave.transform.childCount == 0;
    }

    System.Collections.IEnumerator NextWave()
    {
        waitingForNextWave = true;

        yield return new WaitForSeconds(delayBetweenWaves);

        currentWave++;

        if (currentWave == 2)
        {
            wave1.SetActive(false);
            wave2.SetActive(true);
            Debug.Log("Wave 2!");
        }
        else if (currentWave == 3)
        {
            wave2.SetActive(false);
            wave3.SetActive(true);
            Debug.Log("Wave 3!");
        }

        waitingForNextWave = false;
    }
}

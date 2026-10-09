
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public float respawnInvulnerability = 2f;
    public Text livesText;

    private bool isInvulnerable = false;
    private Renderer playerRenderer;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        playerRenderer = GetComponent<Renderer>();

        UpdateLivesUI();
    }

    public void LoseLife()
    {
        if (isInvulnerable)
            return;

        lives--;

        Debug.Log("Player lost a life. Lives remaining: " + lives);

        UpdateLivesUI();

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(Respawn());
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    IEnumerator Respawn()
    {
        transform.position = startingPosition;
        isInvulnerable = true;

        float elapsed = 0f;

        while (elapsed < respawnInvulnerability)
        {
            if (playerRenderer != null)
                playerRenderer.enabled = !playerRenderer.enabled;

            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        if (playerRenderer != null)
            playerRenderer.enabled = true;

        isInvulnerable = false;
    }
}

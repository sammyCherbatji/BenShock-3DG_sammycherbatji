using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;

    private int score = 0;

    void Start()
    {
        UpdateScore();

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void PlayerLost()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
            gameOverText.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void PlayerWon()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "YOU WIN!";
            gameOverText.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}
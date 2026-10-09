
using UnityEngine;

public class InvaderHealth : MonoBehaviour
{
    public int health = 2;
    public int points = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            ScoreManager scoreManager =
                FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }

            Destroy(gameObject);
        }
    }
}

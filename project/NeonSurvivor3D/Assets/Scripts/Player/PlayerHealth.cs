using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Lives")]
    public int lives = 3;

    [Header("Damage Settings")]
    public int damageAmount = 20;
    public float damageCooldown = 1f;

    [Header("UI")]
    public TMP_Text healthText;
    public TMP_Text livesText;

    public Slider healthSlider;

    private float nextDamageTime;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<EnemyAI>() != null)
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + damageCooldown;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLife();
        }

        UpdateUI();
    }

    private void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
            return;
        }

        currentHealth = maxHealth;
    }

    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "HEALTH: " + currentHealth;

        if (livesText != null)
            livesText.text = "LIVES: " + lives;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
}
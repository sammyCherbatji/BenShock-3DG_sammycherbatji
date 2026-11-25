
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int lives = 3;
    public int damageAmount = 20;
    public float damageCooldown = 1f;

    public TMP_Text healthText;
    public TMP_Text livesText;
    public Slider healthSlider;

    [Header("Shield Settings")]
    public float shieldDuration = 8f;

    private float nextDamageTime;
    private bool shieldActive = false;

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
        if (shieldActive)
        {
            Debug.Log("SHIELD BLOCKED DAMAGE!");
            return;
        }

        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + damageCooldown;

        currentHealth -= damage;

        if (currentHealth <= 0)
            LoseLife();

        UpdateUI();
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateUI();

        Debug.Log("Player restored health: " + currentHealth);
    }

    public void ActivateShield()
    {
        StopCoroutine(nameof(ShieldRoutine));
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        shieldActive = true;

        Debug.Log("SHIELD ACTIVATED!");

        yield return new WaitForSeconds(shieldDuration);

        shieldActive = false;

        Debug.Log("SHIELD EXPIRED!");
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
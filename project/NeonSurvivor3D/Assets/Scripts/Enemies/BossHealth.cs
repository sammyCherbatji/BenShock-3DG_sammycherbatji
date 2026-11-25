
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider healthSlider;
    public GameObject victoryPanel;

    private int currentHealth;
    private bool defeated = false;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (defeated) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            DefeatBoss();
    }

    private void DefeatBoss()
    {
        defeated = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("VICTORY! GAME PAUSED!");

        Destroy(gameObject);
    }
}
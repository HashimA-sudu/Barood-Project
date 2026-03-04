using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public Image healthBarFill; // Drag the Green Image here

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Update the bar (0 to 1 range)
        healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0) Destroy(gameObject);
    }
}
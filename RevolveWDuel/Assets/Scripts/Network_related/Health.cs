using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public delegate void HealthChangedDelegate(int newHealth);
    public event HealthChangedDelegate OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth); // Initial health broadcast
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died");
        // Add respawn or death handling logic here for any object
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}

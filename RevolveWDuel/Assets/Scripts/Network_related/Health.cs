using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public float maxHealth = 100f;
    private NetworkVariable<float> currentHealth = new NetworkVariable<float>();

    private void Start()
    {
        if (IsServer)
        {
            currentHealth.Value = maxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        if (!IsServer) return;

        currentHealth.Value -= amount;
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died");
        // Add respawn or death handling logic here
    }

    public float GetCurrentHealth() => currentHealth.Value;
    public float GetMaxHealth() => maxHealth;
}

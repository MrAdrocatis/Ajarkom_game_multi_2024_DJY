using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PowerUp : NetworkBehaviour
{
    public enum PowerUpType { Ammo, Health }
    public PowerUpType powerUpType;
    public int amount = 10; // Amount to restore for either ammo or health

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerControllerNetwork>(out PlayerControllerNetwork player))
        {
            if (IsServer)
            {
                ApplyPowerUp(player);
                DespawnPowerUp();
            }
        }
    }

    private void ApplyPowerUp(PlayerControllerNetwork player)
    {
        if (powerUpType == PowerUpType.Ammo && player.weapon != null)
        {
            player.weapon.AddAmmo(amount);
        }
        else if (powerUpType == PowerUpType.Health && player.health != null)
        {
            player.health.Heal(amount);
        }
    }

    private void DespawnPowerUp()
    {
        // Optionally add a delay or animation before despawning
        NetworkObject.Despawn();
    }
}

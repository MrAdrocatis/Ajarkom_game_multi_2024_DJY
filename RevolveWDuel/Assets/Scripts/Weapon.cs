using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 10f;       // Reduced bullet speed
    public int maxBullets = 10;         // Maximum bullet count
    public float fireCooldown = 0.5f;   // Cooldown in seconds

    private int currentBullets;
    private float nextFireTime = 0f;    // Tracks when the player can fire next

    private void Start()
    {
        // Initialize current bullets to max bullets at the start
        currentBullets = maxBullets;
    }

    public void Fire()
    {
        // Check if the cooldown period has passed and if there are bullets left
        if (Time.time >= nextFireTime && currentBullets > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            // Decrease bullet count and set the next allowed fire time
            currentBullets--;
            nextFireTime = Time.time + fireCooldown;
        }
        else if (currentBullets <= 0)
        {
            Debug.Log("Out of bullets!");
        }
        else
        {
            Debug.Log("On cooldown!");
        }
    }

    // Optional: Reload function to reset bullet count
    public void Reload()
    {
        currentBullets = maxBullets;
    }
}

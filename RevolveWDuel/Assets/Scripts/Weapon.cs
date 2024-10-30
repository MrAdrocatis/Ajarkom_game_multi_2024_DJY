using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 10f;
    public int maxBullets = 10;
    public float fireCooldown = 0.5f;

    private int currentBullets;
    private float nextFireTime = 0f;
    private AmmoDisplay ammoDisplay;

    private void Start()
    {
        currentBullets = maxBullets;
        ammoDisplay = FindObjectOfType<AmmoDisplay>();

        // Initialize the ammo display
        if (ammoDisplay != null)
        {
            ammoDisplay.UpdateAmmoDisplay(currentBullets);
        }
    }

    public void Fire()
    {
        if (currentBullets > 0 && Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            currentBullets--;
            nextFireTime = Time.time + fireCooldown;

            // Update the ammo display
            if (ammoDisplay != null)
            {
                ammoDisplay.UpdateAmmoDisplay(currentBullets);
            }
        }
        else if (currentBullets <= 0)
        {
            Debug.Log("Out of bullets!");
        }
    }

    // Optional: Reload function to reset bullet count and update display
    public void Reload()
    {
        currentBullets = maxBullets;
        if (ammoDisplay != null)
        {
            ammoDisplay.UpdateAmmoDisplay(currentBullets);
        }
    }
}

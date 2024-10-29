using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f; // Set lifetime in seconds

    private void Start()
    {
        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet on collision
        Destroy(gameObject);

        // Check if hitting an enemy and apply damage logic here
        // DAMAGE ENEMY
    }
}

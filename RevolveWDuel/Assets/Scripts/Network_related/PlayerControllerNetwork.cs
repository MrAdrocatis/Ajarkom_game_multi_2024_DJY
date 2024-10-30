using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControllerNetwork : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    public Health health; // Reference to the health component
    public HealthBarManager healthBar;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        if (IsLocalPlayer)
        {
            healthBar = FindObjectOfType<HealthBarManager>(); // Find and set the health bar UI
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update health bar for the local player
        if (IsLocalPlayer && healthBar != null)
        {
            healthBar.UpdateHealthBar(health.GetCurrentHealth(), health.GetMaxHealth());
        }
    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer) return;

        rb.velocity = moveDirection * moveSpeed;

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}

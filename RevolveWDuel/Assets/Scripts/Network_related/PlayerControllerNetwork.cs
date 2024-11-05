using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControllerNetwork : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return; // Only allow input for the local player

        // Get input for movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Check for firing weapon
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        // Update movement direction based on input
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer) return; // Only move and rotate for the local player

        // Move the player
        rb.velocity = moveDirection * moveSpeed;

        // Calculate the aim direction based on the mouse position
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        // Set the rotation based on the aim direction
        rb.rotation = aimAngle;
    }
}




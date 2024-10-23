using UnityEngine;

public class DashState : BaseState
{
    private float dashTime;
    public float nextDashTime; // Next dash cooldown

    public override void Enter()
    {
        dashTime = stateMachine.dashDuration;
        nextDashTime = Time.time + stateMachine.dashCooldown;

        // Validate dash direction to ensure player can dash only with valid input
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection != Vector2.zero)
        {
            // Apply dash speed to the player
            stateMachine.rb.velocity = dashDirection * stateMachine.dashSpeed;
        }
        else
        {
            // If no valid direction, return to IdleState
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    public override void UpdateLogic()
    {
        dashTime -= Time.deltaTime;
        if (dashTime <= 0)
        {
            stateMachine.ChangeState(stateMachine.idleState); // Kembali ke idle setelah dash selesai
        }
    }

    public override void UpdatePhysics()
    {
        // Physics already handled in Enter for Dash state
    }

    public override void Exit()
    {
        // Reset velocity to zero after dash ends
        stateMachine.rb.velocity = Vector2.zero;
    }
}

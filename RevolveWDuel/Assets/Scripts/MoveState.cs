using UnityEngine;

public class MoveState : BaseState
{
    private float speedX, speedY;

    public override void Enter()
    {
        // Could add any enter logic if needed
    }

    public override void UpdateLogic()
    {
        speedX = Input.GetAxisRaw("Horizontal") * stateMachine.movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * stateMachine.movSpeed;
    }

    public override void UpdatePhysics()
    {
        stateMachine.rb.velocity = new Vector2(speedX, speedY);
    }

    public override void Exit()
    {
        // Could add exit logic if needed
    }
}

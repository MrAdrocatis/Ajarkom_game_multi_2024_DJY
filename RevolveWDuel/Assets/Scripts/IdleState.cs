using UnityEngine;

public class IdleState : BaseState
{
    public override void Enter()
    {
        stateMachine.rb.velocity = Vector2.zero; // Berhenti saat idle
    }

    public override void UpdateLogic()
    {
        // No specific logic for Idle in this example
    }

    public override void UpdatePhysics()
    {
        // Nothing to update in physics for idle
    }

    public override void Exit()
    {
        // No specific exit logic for Idle
    }
}

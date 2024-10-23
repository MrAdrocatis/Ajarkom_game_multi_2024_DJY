using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class MovementSM : MonoBehaviour
{
    public float movSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    [HideInInspector] public Rigidbody2D rb;

    private BaseState currentState;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public DashState dashState = new DashState();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        idleState.Init(this);
        moveState.Init(this);
        dashState.Init(this);

        // Set the initial state
        ChangeState(idleState);
    }

    void Update()
    {
        currentState.UpdateLogic();

        // Transition to dash if Left Shift is pressed and cooldown is finished
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= dashState.nextDashTime)
        {
            ChangeState(dashState);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            // Transition to move if player is giving movement input
            ChangeState(moveState);
        }
        else
        {
            // Transition to idle if no input
            ChangeState(idleState);
        }
    }

    void FixedUpdate()
    {
        currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }
}

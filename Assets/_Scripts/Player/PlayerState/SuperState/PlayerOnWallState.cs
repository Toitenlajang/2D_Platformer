using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isOnWall;
    protected bool grabInput;
    protected bool jumpInput;

    protected int xInput;
    protected int yInput;

    protected bool isTouchingLedge;
    public PlayerOnWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isOnWall = core.CollisionSenses.WallFront;
        isTouchingLedge = core.CollisionSenses.LedgeHorizontal;

        if (isOnWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.InputX;
        yInput = player.InputHandler.InputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isOnWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isOnWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isOnWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

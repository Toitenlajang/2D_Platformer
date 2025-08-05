using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool jumpInput;
    private bool grabInput;
    private bool dashInput;

    private bool isGrounded;
    private bool isOnWall;
    private bool isOnWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;

    private float startWallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isOnWall = core.CollisionSenses.WallFront;
        isOnWallBack = core.CollisionSenses.WallBack;
        isTouchingLedge = core.CollisionSenses.LedgeHorizontal;


        if(isOnWall && !isTouchingLedge)
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

        isOnWall = false;
        isOnWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        jumpInput = player.InputHandler.JumpInput;
        xInput = player.InputHandler.InputX;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (player.InputHandler.AttackInput[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInput[(int)(CombatInputs.secondary)])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(isOnWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && (isOnWall || isOnWallBack))
        {
            player.WallJumpState.DetermineWallJumpDirection(isOnWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isOnWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isOnWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if(dashInput && player.DashState.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;
    public void StartWallJumpCoyoteTime()
    {
        startWallJumpCoyoteTime = Time.time;
        wallJumpCoyoteTime = true;
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
}

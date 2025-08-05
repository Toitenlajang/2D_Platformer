using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int walljumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        core.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, walljumpDirection);
        core.Movement.CheckIfShouldFlip(walljumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

    }
    public void DetermineWallJumpDirection(bool isOnWall)
    {
        if(isOnWall)
        {
            walljumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            walljumpDirection = core.Movement.FacingDirection;
        }
    }
}

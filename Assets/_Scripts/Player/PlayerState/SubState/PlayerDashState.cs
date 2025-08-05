using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash {  get; private set; }

    private Vector2 dashDirection;
    private Vector2 lastAfterImagePos;

    private float lastDashTime;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        dashDirection = Vector2.right * core.Movement.FacingDirection;
    }

    public override void Exit()
    {
        base.Exit();

        if(core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            PlaceAfterImage();
            core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
            CheckIfShouldPlaceAfterImage();

            if(Time.time >= startTime + playerData.dashTime)
            {
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAfterImagePos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePos = player.transform.position;
    }
    public bool CheckIfcanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown;
    }
    public void ResetCanDash() => CanDash = true;
}

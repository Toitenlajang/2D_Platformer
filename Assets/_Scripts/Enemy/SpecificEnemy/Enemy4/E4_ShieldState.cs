using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_ShieldState : State
{
    private Enemy4 enemy;
    private D_ShieldStateData stateData;

    private bool isShieldTimeOver;
    private bool performCloseRangeAction;
    private bool isDetectingLedge;
    private bool isDetectingWall;

    private static float lastShieldEndTime;

    public E4_ShieldState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_ShieldStateData stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();

        isDetectingLedge = core.CollisionSenses.LedgeVertical;
        isDetectingWall = core.CollisionSenses.WallFront;
        
    }

    public override void Enter()
    {
        base.Enter();

        isShieldTimeOver = false;
        core.Movement.SetVelocityX(0f);

        core.Stats.ActivateShield();
    }
    public override void Exit()
    {
        base.Exit();

        lastShieldEndTime = Time.time;

        core.Stats.DeactivateShield();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check if shield duration is over
        if (Time.time >= startTime + stateData.shieldDuration)
        {
            isShieldTimeOver = true;
        }

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isShieldTimeOver)
        {
            stateMachine.ChangeState(enemy.ChargeState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        core.Movement.SetVelocityX(0f);
    }

    public static bool IsOnCoolDown(float coolDownDuration)
    {
        return Time.time < lastShieldEndTime + coolDownDuration;
    }
    
}

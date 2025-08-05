using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ChargeState : ChargeState
{
    private Enemy2 enemy;
    public E2_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void DoChecks()
    {
        base.DoChecks();
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (performLongRangeAction && !isPlayerInMinRange)
        {
            stateMachine.ChangeState(enemy.RangedAttackState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinRange)
            {
                stateMachine.ChangeState(enemy.DetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
        else if (entity.isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

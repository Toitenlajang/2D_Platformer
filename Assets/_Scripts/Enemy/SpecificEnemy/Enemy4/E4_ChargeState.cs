using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_ChargeState : ChargeState
{
    private Enemy4 enemy;

    public E4_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        else if (performLongRangeAction
            && !E4_ShieldState.IsOnCoolDown(enemy.ShieldStateData.shieldCoolDown))
        {
            stateMachine.ChangeState(enemy.ShieldState);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_DetectedState : DetectedState
{
    private Enemy5 enemy;
    public E5_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (performChargeAction)
        {
            stateMachine.ChangeState(enemy.ChargeState);
        }
        else if (!isPlayerInMaxRange)
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_MoveState : MoveState
{
    private Enemy5 enemy;
    public E5_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinRange)
        {
            stateMachine.ChangeState(enemy.DetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.IdleState);
        }
        else if (entity.isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }
}

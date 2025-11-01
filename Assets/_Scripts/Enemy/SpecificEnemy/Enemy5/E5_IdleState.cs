using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_IdleState : IdleState
{
    private Enemy5 enemy;

    public E5_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
        else if (entity.isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }
}

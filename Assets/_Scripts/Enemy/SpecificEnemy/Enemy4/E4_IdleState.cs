using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_IdleState : IdleState
{
    private Enemy4 enemy;
    public E4_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinRange || isPlayerInMaxRange)
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

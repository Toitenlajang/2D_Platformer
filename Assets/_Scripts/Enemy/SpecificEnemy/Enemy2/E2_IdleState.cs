using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : IdleState
{
    private Enemy2 enemy;
    public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

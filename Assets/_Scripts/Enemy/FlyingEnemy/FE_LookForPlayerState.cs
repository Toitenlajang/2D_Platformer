using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_LookForPlayerState : LookForPlayerState
{
    private FlyingEnemy enemy;
    public FE_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = entity.CheckPlayerInMinRange();
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

        if (isAllTurnsDone)
        {
            stateMachine.ChangeState(enemy.FlyingState);
        }
        else if (isPlayerInMinRange)
        {
            stateMachine.ChangeState(enemy.DetectedState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

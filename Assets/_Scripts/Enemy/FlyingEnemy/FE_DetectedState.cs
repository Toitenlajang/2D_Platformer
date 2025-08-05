using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_DetectedState : DetectedState
{
    private FlyingEnemy enemy;

    public FE_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = enemy.CheckPlayerInMinRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
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

        if (performChargeAction)
        {
            stateMachine.ChangeState(enemy.ChargeState);
        }
        else if (performCloseRangeAction) 
        { 
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

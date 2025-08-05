using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_DamagedState : DamagedState
{
    private FlyingEnemy enemy;

    public FE_DamagedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DamagedState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = entity.CheckPlayerInMinRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Core.Movement.CanSetVelocity = true; // Unfreeze movement
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (isDamagedTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }
            else if (isPlayerInMinRange)
            {
                stateMachine.ChangeState(enemy.ChargeState);
            }
            else
            {
                enemy.LookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}

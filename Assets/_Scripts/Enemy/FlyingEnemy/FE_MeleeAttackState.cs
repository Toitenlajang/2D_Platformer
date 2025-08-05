using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_MeleeAttackState : MeleeAttackState
{
    private FlyingEnemy enemy;
    public FE_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = enemy.CheckPlayerInMinRange();
        isPlayerInCloseRange = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isPlayerInCloseRange)
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

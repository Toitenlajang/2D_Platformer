using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DamagedState : DamagedState
{
    private Enemy1 enemy;

    public E1_DamagedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DamagedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        if (!wasKnockback)
        {
            core.Movement.SetVelocityX(0f);
        }
    }

    public override void Exit()
    {
        base.Exit();
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

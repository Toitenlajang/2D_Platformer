using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DetectedState : DetectedState
{
    private Enemy1 enemy;
    public E1_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

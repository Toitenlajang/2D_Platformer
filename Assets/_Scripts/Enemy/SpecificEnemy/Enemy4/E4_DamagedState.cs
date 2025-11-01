using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_DamagedState : DamagedState
{
    private Enemy4 enemy;
    public E4_DamagedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DamagedState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (!wasKnockback)
        {
            core.Movement.SetVelocityX(0f);
        }
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
}

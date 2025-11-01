using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_MeleeAttackState : MeleeAttackState
{
    private Enemy5 enemy;
    public E5_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinRange)
            {
                stateMachine.ChangeState(enemy.DetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}

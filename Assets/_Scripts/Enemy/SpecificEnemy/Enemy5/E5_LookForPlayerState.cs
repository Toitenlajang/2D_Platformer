using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E5_LookForPlayerState : LookForPlayerState
{
    private Enemy5 enemy;
    public E5_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinRange)
        {
            stateMachine.ChangeState(enemy.DetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
        else if (isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }
}

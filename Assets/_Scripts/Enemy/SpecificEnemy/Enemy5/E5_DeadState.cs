using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_DeadState : DeadState
{
    private Enemy5 enemy;
    public E5_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}

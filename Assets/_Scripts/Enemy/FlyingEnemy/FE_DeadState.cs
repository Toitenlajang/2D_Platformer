using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_DeadState : DeadState
{
    private FlyingEnemy enemy;

    public FE_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        core.Movement.Rb.gravityScale = 2f; // Set gravity scale to make the enemy fall

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}

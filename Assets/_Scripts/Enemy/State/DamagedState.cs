using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : State
{
    protected D_DamagedState stateData;

    protected bool isDamagedTimeOver;
    protected bool isGrounded;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMinRange;

    protected Combat combat;
    protected bool wasKnockback;

    public DamagedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DamagedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinRange = entity.CheckPlayerInMinRange();
    }

    public override void Enter()
    {
        base.Enter();

        isDamagedTimeOver = false;

        if(combat != null)
        {
            combat = core.GetComponentInChildren<Combat>();
        }

    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting DamagedState, resetting damaged bool");
       
        entity.isDamaged = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.damagedTime)
        {
            isDamagedTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

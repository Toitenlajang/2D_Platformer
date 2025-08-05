using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedState : State
{
    protected D_DetectedState stateData;

    protected bool isPlayerInMinRange;
    protected bool isPlayerInMaxRange;
    protected bool performLongRangeAction;
    protected bool performChargeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;
    public DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = entity.CheckPlayerInMinRange();
        isPlayerInMaxRange = entity.CheckPlayerInMaxRange();
        isDetectingLedge = core.CollisionSenses.LedgeVertical;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        performLongRangeAction = entity.CheckPlayerInLongRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performChargeAction = false;

        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeActionTime)
        {
            performChargeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}

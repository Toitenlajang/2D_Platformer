using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool isDamaged;
    protected bool turnImmediately;
    protected bool isPlayerInMinRange;
    protected bool isPlayerInMaxRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnDone;
    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = entity.CheckPlayerInMinRange();
        isPlayerInMaxRange =  entity.CheckPlayerInMaxRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnDone = 0;

        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnImmediately)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnDone++;
            turnImmediately = false; 
        }

        else if(Time.time >= lastTurnTime + stateData.timeBetweenTurn && !isAllTurnsDone)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnDone++; 
        }

        if(amountOfTurnDone > stateData.ammountOfTurn)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + stateData.timeBetweenTurn && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}

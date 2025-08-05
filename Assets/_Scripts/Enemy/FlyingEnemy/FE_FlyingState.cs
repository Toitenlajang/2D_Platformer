using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_FlyingState : State
{
    private D_FlyingStateData stateData;
    private FlyingEnemy enemy;

    protected Transform nextWayPoint;

    protected int wayPointNum = 0;
    protected float wayPointReachedDistance = 0.1f;

    public FE_FlyingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FlyingStateData stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        this.enemy = enemy;
    }

    #region Movement variables

    protected bool isDetectingWall;
    protected bool isDetectingGround;

    protected bool isPlayerInMinRange;
    protected bool isplayerInCloseRange;

    protected Vector2 startPosition;
    protected Vector2 targetPosition;
    protected bool movingRight;
    protected bool hasReachedTarget;

    #endregion

    

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = enemy.CheckPlayerInMinRange();
        isplayerInCloseRange = enemy.CheckPlayerInCloseRangeAction();

        isDetectingWall = core.CollisionSenses.WallFront;
        isDetectingGround = core.CollisionSenses.Ground;
    }
    public override void Enter()
    {
        base.Enter();

        nextWayPoint = enemy.wayPoints[wayPointNum];

        startPosition = entity.transform.position;
        SetInitialDirection();
        CalculateTargetPosition();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckForDirectionChange();

        Flight();

        if (isPlayerInMinRange)
        {
            stateMachine.ChangeState(enemy.DetectedState);
        }
        else if (entity.isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void Flight()
    {
        //fly to the next waypoint
        Vector2 directionToWaypoint = (nextWayPoint.position - entity.transform.position).normalized;

        // Check if we had reached the waypoint already
        float distance = Vector2.Distance(nextWayPoint.position, entity.transform.position);

        core.Movement.Rb.velocity = directionToWaypoint * stateData.flyingSpeed;

        // See if we need to switch waypoint
        if (distance <= wayPointReachedDistance)
        {
            //Switch to next wayPoint
            wayPointNum++;

            if (wayPointNum >= enemy.wayPoints.Count)
            {
                wayPointNum = 0;
            }

            nextWayPoint = enemy.wayPoints[wayPointNum];
        }
    }

    #region Flying Logic Functions

    private void SetInitialDirection()
    {
        movingRight = core.Movement.FacingDirection > 0;
    }
    private void CalculateTargetPosition()
    {
        if (movingRight)
        {
            targetPosition = startPosition + Vector2.right * stateData.flyDistance;
        }
        else
        {
            targetPosition = startPosition + Vector2.left * stateData.flyDistance;
        }

        hasReachedTarget = false;
    }
    private void CheckForDirectionChange()
    {
        // Change direction if hitting wall or reaching patrol distance
        if (isDetectingWall || hasReachedTarget)
        {
            FlipDirection();
        }

        // Vertical obstacle avoidance
        //HandleVerticalAvoidance();
    }
    private void FlipDirection()
    {
        movingRight = !movingRight;
        core.Movement.Flip();
        CalculateTargetPosition();
    }

    #endregion
}

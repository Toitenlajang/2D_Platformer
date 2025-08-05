using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_ChargeState : ChargeState
{
    private FlyingEnemy enemy;
    private GameObject player;
    public FE_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, FlyingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = enemy.CheckPlayerInMinRange();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            FacePlayer();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player == null)
        {
            Debug.Log("player in charger state is null");
        }

        Chase();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (isDetectingWall)
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
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
        else if (entity.isDamaged)
        {
            stateMachine.ChangeState(enemy.DamagedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void FacePlayer()
    {
        float directionToPlayer = player.transform.position.x - enemy.transform.position.x;
        if ((directionToPlayer > 0 && enemy.Core.Movement.FacingDirection < 0) ||
            (directionToPlayer < 0 && enemy.Core.Movement.FacingDirection > 0))
        {
            enemy.Core.Movement.Flip();
        }
    }
    private void Chase()
    {

        if (player == null)
        {
            Debug.LogWarning("FE_ChargeState: Player reference is null in Chase()");
            return;
        }

        // Flip to face the player if needed
        float directionToPlayer = player.transform.position.x - enemy.transform.position.x;
        bool shouldFaceRight = directionToPlayer > 0;
        bool currentlyFacingRight = enemy.Core.Movement.FacingDirection > 0;

        if (shouldFaceRight != currentlyFacingRight)
        {
            enemy.Core.Movement.Flip();
        }

        // Calculate direction to player (same as original logic)
        Vector2 directionVector = (player.transform.position - enemy.transform.position).normalized;

        // Override parent's movement to move directly toward player
        // This maintains the original "move toward player" behavior
        enemy.Core.Movement.SetVelocityX(directionVector.x * stateData.chargeSpeed);
        enemy.Core.Movement.SetVelocityY(directionVector.y * stateData.chargeSpeed);

    }
}

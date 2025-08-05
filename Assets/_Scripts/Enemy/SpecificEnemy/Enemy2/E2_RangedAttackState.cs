using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangedAttackState : RangedAttackState
{
    private Enemy2 enemy;

    private Projectiles currentProjectile;
    private float shootTimer;

    private float projectileMoveSpeed;
    private float projectileMaxHeight;
    private float shootRate;

    public E2_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;

        this.projectileMoveSpeed = stateData.projectileSpeed;
        this.projectileMaxHeight = 10f;
        this.shootRate = 2f;
        this.shootTimer = 0f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        shootTimer = 0f;
    }

    public override void Exit()
    {
        base.Exit();

        currentProjectile = null;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Shooter-like timing logic
        UpdateShootTimer();

        if (isAnimationFinished)
        {
            if (isPlayerInMinRange || !isPlayerInMaxRange)
            {
                stateMachine.ChangeState(enemy.DetectedState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();

        // Check if we can shoot
        if (shootTimer <= 0 )
        {
            FireProjectile();
            shootTimer = shootRate;
        }
    }

    public void UpdateShootTimer()
    {
        shootTimer -= Time.deltaTime;

        //Clean up destroyed projecties
        if (currentProjectile != null && currentProjectile.gameObject == null)
        {
            currentProjectile = null;
        }
    }
    public void FireProjectile()
    {
        if (stateData.projectile == null)
        {
            Debug.LogError("Projectile prefab is not assigned in stateData!");
            return;
        }

        // Find the player target
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found");
        }

        // Instantiate the projectile
        GameObject newProjectileObj = GameObject.Instantiate(stateData.projectile, attackPosition.position, Quaternion.identity);
        Projectiles projectileComponent = newProjectileObj.GetComponent<Projectiles>();

        if (projectileComponent == null)
        {
            Debug.LogError("E2_Projectiles script not found on projectile prefab!");
            GameObject.Destroy(newProjectileObj);
            return;
        }

        // Initialize the projectile with trajectory-based movement
        projectileComponent.InitializeProjectile(player.transform, projectileMoveSpeed, projectileMaxHeight);

        // keep reference to current projectile
        currentProjectile = projectileComponent;
    }


}

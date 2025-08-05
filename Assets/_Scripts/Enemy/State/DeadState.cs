using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.StartCoroutineFromState(DisableColliderAfterDelay(0.2f));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private IEnumerator DisableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        var combat = core.Combat;
        if (combat != null)
        {
            var collider = combat.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}

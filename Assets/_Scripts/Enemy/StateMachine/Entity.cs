using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Entity : MonoBehaviour, IDamageable
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection {  get; private set; }
    public Core Core { get; private set; }
    public Stats stats => Core.Stats;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    private Vector2 velocityWorkspace;

    public bool isDamaged;
    public bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDist, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInLongRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.longrangeActionDist, entityData.whatIsPlayer);
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        Damage(attackDetails.damageAmount);
    }
    public virtual void Damage(float amount)
    {
        stats.DecreaseHealth(amount);

        isDamaged = true;
        if (stats.currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("enemy is dead");
        }
    }
    public void StartCoroutineFromState(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
    public virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
         Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
         Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDist), 0.2f);
         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.longrangeActionDist), 0.2f);
         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minDistance), 0.2f);
         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxDistance), 0.2f);
        }
    }
}

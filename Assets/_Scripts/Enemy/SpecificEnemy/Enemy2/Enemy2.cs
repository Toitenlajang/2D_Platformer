using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState IdleState { get; private set; }
    public E2_MoveState MoveState { get; private set; }
    public E2_DetectedState DetectedState { get; private set; }
    public E2_ChargeState ChargeState { get; private set; }
    public E2_LookForPlayerState LookForPlayerState { get; private set; }
    public E2_DamagedState DamagedState { get; private set; }
    public E2_MeleeAttackState MeleeAttackState { get; private set; }
    public E2_DeadState DeadState { get; private set; }
    public E2_RangedAttackState RangedAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_DetectedState detectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_DamagedState damagedStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        DetectedState = new E2_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        ChargeState = new E2_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        DamagedState = new E2_DamagedState(this, stateMachine, "damaged", damagedStateData, this);
        MeleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        DeadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        RangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

    }
    public void Start()
    {
        stateMachine.Initialize(MoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadious);

    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDamaged)
        {
            stateMachine.ChangeState(DamagedState);
        }
        if (isDead)
        {
            stateMachine.ChangeState(DeadState);
        }
        if (anim != null)
        {
            anim.Play("E2_Damaged", -1, 0f);
        }
        Debug.Log("Damage void is called on enemy2");
    }
}

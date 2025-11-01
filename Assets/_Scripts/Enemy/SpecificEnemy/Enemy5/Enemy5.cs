using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Entity
{
    public E5_IdleState IdleState { get; private set; }
    public E5_MoveState MoveState { get; private set; }
    public E5_ChargeState ChargeState { get; private set; }
    public E5_DetectedState DetectedState { get; private set; }
    public E5_LookForPlayerState LookForPlayerState { get; private set; }
    public E5_MeleeAttackState MeleeAttackState { get; private set; }
    public E5_DamagedState DamagedState { get; private set; }
    public E5_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_DetectedState detectedStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DamagedState damagedStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E5_IdleState(this, stateMachine, "idle", idleStateData, this);
        MoveState = new E5_MoveState(this, stateMachine, "move", moveStateData, this);
        ChargeState = new E5_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        DetectedState = new E5_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        LookForPlayerState = new E5_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        MeleeAttackState = new E5_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        DamagedState = new E5_DamagedState(this, stateMachine, "damaged", damagedStateData, this);
        DeadState = new E5_DeadState(this, stateMachine, "dead", deadStateData, this);

    }
    public void Start()
    {
        stateMachine.Initialize(MoveState);
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
            anim.Play("E5_Damaged", -1, 0f);
        }
        Debug.Log("Damage void is called on enemy5");
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadious);
    }
}

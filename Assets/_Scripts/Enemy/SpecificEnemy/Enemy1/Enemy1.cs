using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState {  get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_DetectedState DetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }
    public E1_MeleeAttackState MeleeAttackState { get; private set; }
    public E1_DamagedState DamagedState { get; private set; }
    public E1_DeadState DeadState { get; private set; }

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

        MoveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateMachine,"idle", idleStateData, this);
        DetectedState = new E1_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        ChargeState = new E1_ChargeState (this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        MeleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        DamagedState = new E1_DamagedState(this, stateMachine, "damaged", damagedStateData, this);
        DeadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);
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
            anim.Play("E1_Damaged", -1, 0f);
        }
        Debug.Log("Damage void is called on enemy1");
    }
}

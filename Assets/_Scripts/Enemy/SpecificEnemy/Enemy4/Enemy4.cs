using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Enemy4 : Entity
{
    public E4_IdleState IdleState { get; private set; }
    public E4_MoveState MoveState { get; private set; }
    public E4_DetectedState DetectedState { get; private set; }
    public E4_ChargeState ChargeState { get; private set; }
    public E4_LookForPlayerState LookForPlayerState { get; private set; }
    public E4_MeleeAttackState MeleeAttackState { get; private set; }
    public E4_DamagedState DamagedState { get; private set; }
    public E4_DeadState DeadState { get; private set; }
    public E4_ShieldState ShieldState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_DetectedState detectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DamagedState damagedStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_ShieldStateData shieldStateData;

    public D_ShieldStateData ShieldStateData => shieldStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E4_IdleState(this, stateMachine, "idle", idleStateData, this);
        MoveState = new E4_MoveState(this, stateMachine, "move", moveStateData, this);
        DetectedState = new E4_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        ChargeState = new E4_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new E4_LookForPlayerState(this, stateMachine, "look", lookForPlayerData, this);
        MeleeAttackState = new E4_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        DamagedState = new E4_DamagedState(this, stateMachine, "damage", damagedStateData, this);
        DeadState = new E4_DeadState(this, stateMachine, "dead", deadStateData, this);
        ShieldState = new E4_ShieldState(this, stateMachine, "shield", shieldStateData, this);
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

    public override void Damage(float amount)
    {
        // Check if enemy is currently shielding
        if (stateMachine.currentState == ShieldState)
        {

            return;
        }

        base.Damage(amount);

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
            anim.Play("E4_Damaged", -1, 0f);
        }
        Debug.Log("Damage void is called on enemy4");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{
    public List<Transform> wayPoints;
    public List<Transform> WayPoints => wayPoints;


    //[SerializeField]
    //private float maxHealth = 25f;
    //protected override float MaxHealth => maxHealth;

    public FE_FlyingState FlyingState { get; private set; }
    public FE_DetectedState DetectedState { get; private set; }
    public FE_ChargeState ChargeState { get; private set; }
    public FE_LookForPlayerState LookForPlayerState { get; private set; }
    public FE_MeleeAttackState MeleeAttackState { get; private set; }
    public FE_DamagedState DamagedState { get; private set; }
    public FE_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_FlyingStateData flyingStateData;
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

    // Collider-based detection variables
    private bool playerInMinRange;
    private bool playerInCloseRange;

    public override void Awake()
    {
        base.Awake();

        FlyingState = new FE_FlyingState(this, stateMachine, "flying", flyingStateData, this);
        DetectedState = new FE_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        ChargeState = new FE_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new FE_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        MeleeAttackState = new FE_MeleeAttackState(this, stateMachine, "attack", meleeAttackPosition, meleeAttackStateData, this);
        DamagedState = new FE_DamagedState(this, stateMachine, "damaged", damagedStateData, this);
        DeadState = new FE_DeadState(this, stateMachine, "dead", deadStateData, this);

    }
    public void Start()
    {
        stateMachine.Initialize(FlyingState);
        
    }

    #region Detection Methods
    // Override only the detection methods that are actually used
    public override bool CheckPlayerInMinRange()
    {
        return playerInMinRange;
    }
    public override bool CheckPlayerInCloseRangeAction()
    {
        return playerInCloseRange;
    }

    // Call methods in the individual detector scripts
    public void OnMinRangeEnter()
    {
        playerInMinRange = true;
        Debug.Log("[FlyingEnemy] Player entered Min Range.");
    }
    public void OnMinRangeExit()
    {
        playerInMinRange = false;
        Debug.Log("[FlyingEnemy] Player exited Min Range.");
    }
    public void OnCloseRangeEnter()
    {
        playerInCloseRange = true;
        Debug.Log("[FlyingEnemy] Player entered Close Range.");
    }
    public void OnCloseRangeExit()
    {
        playerInCloseRange = false;
        Debug.Log("[FlyingEnemy] Player exited Close Range.");
    }
    #endregion


    public override void OnDrawGizmos()
    {
        
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
        Debug.Log("Damage void is called on flying enemy");
    }
}

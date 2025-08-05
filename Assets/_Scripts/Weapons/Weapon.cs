using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    private List<IDamageable> detectedDamageable = new List<IDamageable>();

    private List<IKnockbackable>detectedKnockbackable = new List<IKnockbackable>();

    public Animator weaponAnimator;

    protected PlayerAttackState state;

    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        weaponAnimator = GetComponent<Animator>();


        gameObject.SetActive(false);
    }

    #region Weapon Fuctions
    private void CheckMeleeAttack()
    {
        if (detectedDamageable.Count == 0)
        {
            Debug.Log("No detected damageable objects");
        }

        AttackDetails details = weaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageable.ToList())
        {
            item.Damage(details.damageAmount);
        }

        foreach (IKnockbackable item in detectedKnockbackable.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.FacingDirection);
        }
    }

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        
            if (attackCounter >= weaponData.amountOfAttacks)
            {
                attackCounter = 0;
            }

        weaponAnimator.SetBool("attack", true);

        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        weaponAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    #endregion

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {
        CheckMeleeAttack();
    }

    #endregion

    #region Weapon HitBox Function
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Add(damageable);

        }

        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();

        if (knockbackable != null)
        {
            detectedKnockbackable.Add(knockbackable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }

        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();

        if (knockbackable != null)
        {
            detectedKnockbackable.Remove(knockbackable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddToDetected(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFromDetected(collision);
    }

    #endregion

}

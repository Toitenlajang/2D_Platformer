using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private float maxKnockbackTime = 0.2f;

    private Entity entity;
    private Player player;

    [SerializeField]
    public HealthBar healthBar;

    private bool isKnockBackActive;
    private float knockbackStartTime;
    protected override void Awake()
    {
        base.Awake();

        entity = core.transform.parent.GetComponent<Entity>();
        player = core.transform.parent.GetComponent<Player>();
    }
    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    //expose the needed functionality via public property
    public Stats GetStats()
    {
        return core.Stats;
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " get hit " + amount + " damaged! ");
        core.Stats.DecreaseHealth(amount);

        
        if (healthBar != null)
        {
            healthBar.SetValue(core.Stats.currentHealth);
        }

        if (entity != null)
        {
            var attackDetails = new AttackDetails
            {
                damageAmount = amount
            };
            entity.Damage(attackDetails);
            CharacterEvents.characterDamaged.Invoke(gameObject, amount);
        }
        if(player != null)
        {
            var attackDetails = new AttackDetails
            {
                damageAmount = amount
            };
            player.Damage(attackDetails);
            CharacterEvents.characterDamaged.Invoke(gameObject, amount);
        }
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        isKnockBackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if(isKnockBackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockBackActive = false;
            core.Movement.CanSetVelocity = true;
            core.Movement.SetVelocityZero();
        }
    }
}

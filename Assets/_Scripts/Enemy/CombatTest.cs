using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CombatTest : MonoBehaviour, IDamageable, IKnockbackable
{
    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + "damage taken");
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Debug.Log( "player is being knockback");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}

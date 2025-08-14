using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damageAmount = 20f;
    public float knockbackStrength = 5f;
    public Vector2 knockbackAngle = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageAmount);
        }

        IKnockbackable knockbackable = other.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            int direction = other.transform.position.x > transform.position.x ? 1 : -1;
            knockbackable.Knockback(knockbackAngle, knockbackStrength, direction);
        }
    }
}

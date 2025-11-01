using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    public float maxHealth;
    public float currentHealth;

    private float shieldHealth; // Store health when shield is activated

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Health is zero");
        }
    }

    public void IncreaseHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        CharacterEvents.characterHealed.Invoke(gameObject, amount);
    }
    public void ActivateShield()
    {
        // Save health when shield is activated
        shieldHealth = currentHealth;
    }
    public void DeactivateShield()
    {
        //Restore health when shield is deactivated
        currentHealth = shieldHealth;
    }

}

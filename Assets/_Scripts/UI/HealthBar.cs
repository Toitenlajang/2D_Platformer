using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Stats health;
    [SerializeField]
    private Image healthBarFill;
    [SerializeField]
    private TMP_Text hpIndicator;

    private float maxRightMask;
    private float initialRightMask;

    private void Start()
    {
        // Initialize the health bar
        if (health != null)
        {
            hpIndicator.SetText(sourceText: $"{health.currentHealth}/ {health.maxHealth}");
            SetValue(health.currentHealth);
        }
        else
        {
            Debug.LogWarning("healthBar is not assigned");
        }
    }
    private void Update()
    {
        if(health != null)
        {
            SetValue(health.currentHealth);
        }
    }
    public void SetValue(float newValue)
    {
        if( health != null && healthBarFill != null)
        {
            // Calculate how much of the bar should be visible
            var healthPercentage = newValue / health.maxHealth;

            //Clamp the value between 0 and 1 to premvent errors;
            healthPercentage = Mathf.Clamp01(healthPercentage);

            //Set the fill amount
            healthBarFill.fillAmount = healthPercentage;

            //Update the text
            hpIndicator.SetText(sourceText: $"{newValue:F0} / {health.maxHealth:F0}"); // Add F0 to remove the decimal

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healAmount = 20f;
    //Spin effect for item
    //public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    //void Update()
    //{
    //    transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the colliding object has a Combat component
        Combat combat = other.GetComponent<Combat>();

        if (combat != null )
        {
            var stats = combat.GetStats();
            if (stats.currentHealth < stats.maxHealth)
            {
                //Heal the player by increasing health
                stats.IncreaseHealth(healAmount);

                //Update the health bar
                if (combat.healthBar != null)
                {
                    combat.healthBar.SetValue(combat.GetStats().currentHealth);
                }

                //Destroy the item
                Destroy(gameObject);
            }   
        }
    }
}

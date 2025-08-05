using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float projectileMoveSpeed;
    [SerializeField]
    private float projectileMaxHeight;

    [SerializeField]
    private float shootRate;
    [SerializeField]
    private float shootTimer;

    private Projectiles currentProjectile;

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && currentProjectile == null)
        {
            shootTimer = shootRate;

            if (projectile == null)
            {
                Debug.LogError("Projectile prefab is not assigned!");
                return;
            }

            if (target == null)
            {
                Debug.LogError("Target is not assigned!");
                return;
            }

            GameObject newProjectileObj = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectiles projectileComponent = newProjectileObj.GetComponent<Projectiles>();

            if (projectileComponent == null)
            {
                Debug.LogError("E2_Projectiles script not found on projectile prefab!");
                Destroy(newProjectileObj);
                return;
            }

            projectileComponent.InitializeProjectile(target, projectileMoveSpeed, projectileMaxHeight);

            currentProjectile = projectileComponent;

            //Clear reference when when the projectile is destrouyed
            if (currentProjectile != null && currentProjectile.gameObject == null)
            {
                currentProjectile = null;
            }
        }
    }
}

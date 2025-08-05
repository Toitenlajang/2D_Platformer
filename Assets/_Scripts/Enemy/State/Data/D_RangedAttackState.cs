using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/Entity Data/State Data/ Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    [Header("Projectile Settings")]
    public GameObject projectile;

    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 10f;
    public float projectileDamage = 10f;

    [Header("Trajectory Settings")]
    public float projectileMaxHeight = 10f;
    public float shootRate = 2f;

    [Header("Attack Settings")]
    public float attackRadious = 1.5f;
}

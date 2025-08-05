using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [SerializeField] private AttackDetails[] attackDetails;
    public AttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    public int amountOfAttacks {  get; protected set; }
    public float[] movementSpeed { get; protected set; }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}

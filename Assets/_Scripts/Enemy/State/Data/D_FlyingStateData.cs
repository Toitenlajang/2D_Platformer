using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new FlyingStateData", menuName = "Data/Entity Data/State Data/ Flying State")]
public class D_FlyingStateData : ScriptableObject
{
    [Header("Movement")]
    public float flyingSpeed = 3f;
    public float flyDistance = 5f; //how far the enemy fly in each direction
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ShieldStateData", menuName = "Data/Entity Data/State Data/ Shield State")]
public class D_ShieldStateData : ScriptableObject
{
    public float shieldDuration = 3f;
    public float shieldCoolDown = 3f;
    public float damageReduction = -1f;
}

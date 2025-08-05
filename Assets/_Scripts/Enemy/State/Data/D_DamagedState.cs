using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDamagedStateData", menuName = "Data/Entity Data/State Data/ Damaged State")]
public class D_DamagedState : ScriptableObject
{
    public float damagedTime = 1f;

    public float knockbackSpeed = 5f;

    public Vector2 knockbackAngle;

}

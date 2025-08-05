using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EntityData", menuName ="Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadious = 0.3f;

    public float maxDistance = 5f;
    public float minDistance = 3f;

    public float closeRangeActionDist = 1f;
    public float longrangeActionDist = 4f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

}

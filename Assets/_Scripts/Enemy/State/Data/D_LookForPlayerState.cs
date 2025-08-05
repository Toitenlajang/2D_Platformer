using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/Entity Data/State Data/ Look For Player State")]
public class D_LookForPlayerState : ScriptableObject
{
    public int ammountOfTurn = 2;
    public float timeBetweenTurn = 0.75f;
}

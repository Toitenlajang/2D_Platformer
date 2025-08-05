using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerDataMenu", menuName ="Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header(" Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 startOffSet;
    public Vector2 stopOffSet;

    [Header("Dash State")]
    public float dashCoolDown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Crouch State")]
    public float crouchMovementVelocity = 10f;
    public float crouchColliderHeight = 1.3f;
    public float standColliderHeight = 1.83f;

    [Header("Damaged State")]
    public float damagedTime = 0.3f;
}

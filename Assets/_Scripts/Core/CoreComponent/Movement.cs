using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    #region Variables
    public Rigidbody2D Rb;
    public Vector2 CurrentVelocity {  get; private set; }
    private Vector2 workSpace;

    public int FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        Rb = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
    }
    public override void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }

    #region Flip functions
    public void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    // Helper function to check if shoulf flip based on target direction
    public void ShouldFlipToward(Vector2 targetDirection)
    {
        if (targetDirection.x > 0 && FacingDirection < 0)
        {
            Flip();
        }
        else if (targetDirection.x < 0 && FacingDirection > 0)
        {
            Flip();
        }
    }
    #endregion

    #region Set Functions
    public void SetVelocityZero()
    {
        workSpace = Vector2.zero;
        SetFinalVelocity();
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        SetFinalVelocity();
    }
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }
    public void SetVelocityY(float velocity)
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }
    public void SetFlyingVelocity(Vector2 targetPosition, float speed)
    {
        Vector2 currentPosition = Rb.transform.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        workSpace = direction * speed;
        SetFinalVelocity();
    }
    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rb.velocity = workSpace;
            CurrentVelocity = workSpace;
        }
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion
    
}

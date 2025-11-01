using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms
    public Transform GroundCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, transform.parent.name);
        private set => groundCheck = value; 
    }
    public Transform WallCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, transform.parent.name);
        private set => wallCheck = value; 
    }
    public Transform LedgeCheckHorizontal 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, transform.parent.name);
        private set => ledgeCheckVertical = value; 
    }
    public Transform CeilingCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, transform.parent.name);
        private set => ceilingCheck = value; 
    }
    public float GroundCheckRadious { get => groundCheckRadious; set => groundCheckRadious = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadious;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;

    #endregion

    #region Check Functions
    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadious, WhatIsGround);
    }
    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadious, WhatIsGround);
    }
    public bool WallFront
    {
        get 
        {
            if (WallCheck == null)
            {
                Debug.LogError("WallCheck is null");
                return false;
            }
            if (core == null)
            {
                Debug.LogError("core is null");
                return false;
            }
            return Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, WhatIsGround); 
        }
    }
    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, WhatIsGround);
    }
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, WhatIsGround);
    }
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, WhatIsGround);
    }

    #endregion
}

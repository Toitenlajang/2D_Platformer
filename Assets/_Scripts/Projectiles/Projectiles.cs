using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Projectiles : MonoBehaviour
{
    private Transform target;
    private Animator anim;
    private Rigidbody2D rb;

    private bool hasExploded = false;
    private bool hasBounced = false;
    private bool isFollowingTrajectory = false;
    private bool isInitialized = false;

    private float moveSpeed;
    private float trajectoryMaxHeight = 20f;

    private Vector3 trajectoryStartPoint;
    private Vector3 targetPosition;

    private float totalDistance;
    private float trajectoryDuration;
    private float elapsedTime = 0f;

    [Header("Projectile Settings")]
    [SerializeField]
    private float groundY = 0f;
    [SerializeField]
    private float bounceForce = 5f;
    [SerializeField]
    private float bounceHorizontalDamping = 0.7f;
    [SerializeField]
    private float gravity = 9.81f;

    [Header("ExplosionSettings")]
    [SerializeField]
    private float explosioRadious = 5f;
    [SerializeField]
    private float explosionDamage = 10f; // Damage dealt on explosion
    [SerializeField]
    private float knockbackStrength = 5f; // Knockback strength on explosion
    [SerializeField]
    private Vector2 knockbackAngle = new Vector2(1, 1); // Knockback angle on explosion
    [SerializeField]
    private LayerMask whatIsPlayer; // Layer mask for player detection


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        if (!isInitialized) return;

        if (!hasExploded && isFollowingTrajectory)
        {
            UpdateProjectilePosition();
        }
    }

    public void UpdateProjectilePosition()
    {
        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate normalized progress (0 to 1)
        float progress = Mathf.Clamp01(elapsedTime / trajectoryDuration);

        // Get the horizontal position (lerp between start and target)
        Vector3 horizontalPosition = Vector3.Lerp(trajectoryStartPoint, targetPosition, progress);

        // Calculate vertical offset based on selected trajectory type
        float verticalOffset = CalculateTrajectoryHeight(progress) * trajectoryMaxHeight;

        // Apply position
        Vector3 newPosition = new Vector3(horizontalPosition.x, trajectoryStartPoint.y + verticalOffset, horizontalPosition.z);
        transform.position = newPosition;

        // If we've completed the trajectory and haven't hit anything, start falling
        if (progress >= 1f && !hasBounced)
        {
            isFollowingTrajectory = false;
            // Enable physics for falling
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.gravityScale = gravity / 9.81f;
                // Give it some downward velocity to start falling
                rb.velocity = new Vector2(0, -5f);
            }
        }
    }
    private float CalculateTrajectoryHeight(float progress)
    {
        return -4f * progress * (progress - 1f);
    }
    public void InitializeProjectile(Transform target, float moveSpeed, float trajectoryMaxHeight)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.trajectoryMaxHeight = trajectoryMaxHeight;

        trajectoryStartPoint = transform.position;

        // Calculate trajectory to target position
        targetPosition = new Vector3(target.position.x, groundY, target.position.z);
        totalDistance = Vector3.Distance(trajectoryStartPoint, targetPosition);

        // This ensures the projectile always moves at the same visual speed
        trajectoryDuration = totalDistance / moveSpeed;

        isInitialized = true;
        isFollowingTrajectory = true;
        elapsedTime = 0f;
    }
    public void Explode()
    {
        hasExploded = true;
        isFollowingTrajectory = false;

        // Stop any physics movement
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        anim.SetTrigger("explode");
        Destroy(gameObject, 0.8f); // Destroy after animation plays
    }
    
    public void ApplyExplosion()
    {
        //Apply damage and knockback logic

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, explosioRadious, whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(explosionDamage);
            }

            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

            if (knockbackable != null)
            {
                int direction = collider.transform.position.x > transform.position.x ? 1 : -1; // Determine direction based on position
                knockbackable.Knockback(knockbackAngle, knockbackStrength, direction);
            }
        }
    }
    public void PerformBounce(Vector2 bounceDirection)
    {
        hasBounced = true;
        isFollowingTrajectory = false;

        // Enable physics for bouncing
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.gravityScale = gravity / 9.81f; // Normalize gravity

            // Apply bounce force
            Vector2 bounceVelocity = new Vector2(
                bounceDirection.x * bounceHorizontalDamping * bounceForce,
                bounceForce
            );

            rb.velocity = bounceVelocity;
        }
    }

    // Helper method to determine bounce direction based on collision
    private Vector2 CalculateBounceDirection(Collision2D collision)
    {
        // Get the collision normal (surface normal)
        Vector2 normal = collision.contacts[0].normal;

        // Get current velocity direction (or movement direction)
        Vector2 incomingDirection;
        if (rb != null && !rb.isKinematic)
        {
            incomingDirection = rb.velocity.normalized;
        }
        else
        {
            // If kinematic, calculate direction based on movement toward target
            incomingDirection = (targetPosition - transform.position).normalized;
        }

        // Calculate reflection using Vector2.Reflect
        Vector2 reflectedDirection = Vector2.Reflect(incomingDirection, normal);

        // Ensure there's always some upward component for a good bounce
        if (reflectedDirection.y < 0.3f)
        {
            reflectedDirection.y = Mathf.Max(0.3f, Mathf.Abs(reflectedDirection.y));
            reflectedDirection.Normalize();
        }

        return reflectedDirection;
    }

    // Alternative method for trigger-based collision (less precise)
    private Vector2 CalculateBounceDirectionFromPosition(Collider2D other)
    {
        // Get the bounds of both colliders
        Bounds projectileBounds = GetComponent<Collider2D>().bounds;
        Bounds otherBounds = other.bounds;

        Vector2 bounceDirection = Vector2.up; // Default upward bounce

        // Determine which edge we hit based on overlap
        float overlapLeft = projectileBounds.max.x - otherBounds.min.x;
        float overlapRight = otherBounds.max.x - projectileBounds.min.x;
        float overlapTop = otherBounds.max.y - projectileBounds.min.y;
        float overlapBottom = projectileBounds.max.y - otherBounds.min.y;

        // Find the smallest overlap to determine collision edge
        float minOverlap = Mathf.Min(overlapLeft, overlapRight, overlapTop, overlapBottom);

        if (minOverlap == overlapTop)
        {
            // Hit top edge - bounce downward (shouldn't happen often)
            bounceDirection = Vector2.down;
        }
        else if (minOverlap == overlapBottom)
        {
            // Hit bottom edge - bounce upward
            bounceDirection = Vector2.up;
        }
        else if (minOverlap == overlapLeft)
        {
            // Hit left edge - bounce left and up
            bounceDirection = new Vector2(-0.7f, 0.7f).normalized;
        }
        else if (minOverlap == overlapRight)
        {
            // Hit right edge - bounce right and up
            bounceDirection = new Vector2(0.7f, 0.7f).normalized;
        }

        return bounceDirection;
    }

    // Collision detection for bouncing and exploding
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore triggers and non-solid objects
        if (other.isTrigger) return;

        if (other.CompareTag("Ground") || other.CompareTag("Player"))
        {

            if (!hasBounced && !hasExploded)
            {
                // First impact - bounce
                Vector2 bounceDirection = CalculateBounceDirectionFromPosition(other);
                PerformBounce(bounceDirection);
            }
            else if (hasBounced && !hasExploded)
            {
                // Second impact - explode
                Explode();
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Elevator Settings")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool autoReturn = false;
    [SerializeField] private float returnDelay = 2f;

    [Header("Trigger Settings")]
    [SerializeField] private bool requirePlayerInput = true;
    [SerializeField] private KeyCode activationKey = KeyCode.E;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject promptUI; // Optional UI prompt

    [Header("Visual Feedback")]
    [SerializeField] private Color inactiveColor = Color.yellow;
    [SerializeField] private Color activeColor = Color.green;

    private bool isActivated = false;
    private bool isMoving = false;
    private bool playerOnPlatform = false;
    private bool isAtEndPosition = false;
    private float returnTimer = 0f;
    private Vector2 targetPosition;
    private Transform playerTransform;
    private Vector2 lastPlatformPosition;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRb;

    private void Start()
    {
        if (startPosition == null || endPosition == null)
        {
            enabled = false;
            return;
        }

        // Set initial position to start
        transform.position = startPosition.position;
        targetPosition = endPosition.position;
        lastPlatformPosition = transform.position;

        // Get sprite renderer for visual feedback
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = inactiveColor;
        }
        // Ensure platform has Rigidbody2D set to Kinematic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }
    private void Update()
    {
        // Handle player input if required
        if (requirePlayerInput && playerOnPlatform && !isActivated)
        {
            // Show prompt if available
            if (promptUI != null && !promptUI.activeSelf)
            {
                promptUI.SetActive(true);
            }

            if (Input.GetKeyDown(activationKey))
            {
                ActivateElevator();

                // Hide prompt after activation
                if (promptUI != null)
                {
                    promptUI.SetActive(false);
                }
            }
        }
        // Move the platform
        if (isMoving)
        {
            MovePlatform();
        }
        // Handle auto return
        if (autoReturn && isActivated && !isMoving && isAtEndPosition)
        {
            returnTimer += Time.deltaTime;
            if (returnTimer >= returnDelay)
            {
                ReturnToStart();
            }
        }
    }
    private void LateUpdate()
    {
        // Move player with platform
        if (playerOnPlatform && playerTransform != null)
        {
            Vector2 platformMovement = (Vector2)transform.position - lastPlatformPosition;

            // Move player using Rigidbody2D if available
            if (playerRb != null)
            {
                playerRb.position += platformMovement;
            }
            else
            {
                playerTransform.position += (Vector3)platformMovement;
            }
        }

        lastPlatformPosition = transform.position;
    }
    private void MovePlatform()
    {
        // Move towards target
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Check if reached target
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
            returnTimer = 0f;

            isAtEndPosition = Vector2.Distance(transform.position, endPosition.position) < 0.01f;
        }
    }
    private void ActivateElevator()
    {
        if (!isActivated)
        {
            isActivated = true;
            isMoving = true;
            targetPosition = endPosition.position;
            isAtEndPosition = false;

            // Visual feedback
            if (spriteRenderer != null)
            {
                spriteRenderer.color = activeColor;
            }

        }
    }
    private void ReturnToStart()
    {
        isActivated = false;
        isMoving = true;
        targetPosition = startPosition.position;
        returnTimer = 0f;

        // Visual feedback
        if (spriteRenderer != null)
        {
            spriteRenderer.color = inactiveColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Check if player is landing on top of platform
            if (collision.transform.position.y > transform.position.y)
            {
                playerOnPlatform = true;
                playerTransform = collision.transform;
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                // Auto-activate if input is not required
                if (!requirePlayerInput && !isActivated)
                {
                    ActivateElevator();
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Simple check: Is player above the platform?
            if (collision.transform.position.y > transform.position.y)
            {
                // Player is above the platform
                if (!playerOnPlatform)
                {
                    playerOnPlatform = true;
                    playerTransform = collision.transform;
                    playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                }
            }
            else
            {
                // Player is not above (probably hitting from side/below)
                if (playerOnPlatform)
                {
                    playerOnPlatform = false;
                    playerTransform = null;
                    playerRb = null;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            playerOnPlatform = false;
            playerTransform = null;
            playerRb = null;

            // Hide prompt when player leaves
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }

}

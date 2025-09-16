using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerStateHandler playerStateHandler;
    private PlayerMovementStats playerMovementStats;

    [Header("Basic Movement")]
    private Vector2 movementInput = Vector2.zero;
    [SerializeField] private float counterForce = 0.5f;
    private float threshold = 0.01f;
    private float currentSpeed;
    private float currentMax;
    [SerializeField] private float dirWallLenght;

    [Header("Jump")]
    private bool isJumping;
    private bool isOnGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float jumpTimerDuration;
    private float jumpTimer;


    [Header("Crouch")]
    [SerializeField] private GameObject topChild;
    [SerializeField] private GameObject bottomChild;
    [SerializeField] private LayerMask layerObstacleDetection;
    [SerializeField] private float detectionLenght;
    private bool hasObjectUp;
    private Collider topCollider;
    private Collider bottomCollider;

    [Header("Slope")]
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopeHit;
    [SerializeField] private float minSlopeYVelocity;
    [SerializeField] private float maxSlopeYVelocity;
    [SerializeField] private bool isOnSlope;
    [SerializeField] private bool wasOnSlope;
    [SerializeField] private float slopeDistance;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovementStats = GetComponent<PlayerMovementStats>();
        topCollider = topChild.GetComponent<Collider>();
        bottomCollider = bottomChild.GetComponent<Collider>();
        playerStateHandler = GetComponent<PlayerStateHandler>();
    }

    void Update()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerObstacleDetection);
        hasObjectUp = Physics.Raycast(bottomChild.transform.position, Vector3.up, detectionLenght, layerObstacleDetection);
        HandleInput();
        MovementStateSpeed();
        UpdateSlopeStatus();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("isOnGround: " + isOnGround);
            Debug.Log("isJumping: " + isJumping);
        }


        if (isOnGround && !Input.GetKey(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        Crouch();
        Jump();
        LimitSpeed();
        Fall();

    }
    //Handle Input
    private void HandleInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        if (playerStateHandler.GetState() != PlayerStates.SLIDING)
        {

            if (Input.GetKey(KeyCode.LeftControl) || hasObjectUp)
            {
                playerStateHandler.SetState(PlayerStates.CROUCHING);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                playerStateHandler.SetState(PlayerStates.SPRINTING);

            }
            else
            {
                playerStateHandler.SetState(PlayerStates.DEFAULT);
            }
            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround ||
                                                    isOnSlope ||
                                                    wasOnSlope
                                                    ))
            {
                isJumping = true;
            }
            else if ((!isOnSlope || !wasOnSlope) && jumpTimer >= jumpTimerDuration)
            {
                isJumping = false;
                jumpTimer = 0;
            }
        }
    }
    //Check current state
    private void MovementStateSpeed()
    {
        if (playerStateHandler.GetState() == PlayerStates.SPRINTING)
        {
            currentSpeed = playerMovementStats.sprintSpeed;
            currentMax = playerMovementStats.maxSprintSpeed;
        }
        else if (playerStateHandler.GetState() == PlayerStates.CROUCHING)
        {
            currentSpeed = playerMovementStats.crouchSpeed;
            currentMax = playerMovementStats.maxCrouchSpeed;
        }
        else if (playerStateHandler.GetState() == PlayerStates.DEFAULT)
        {
            currentSpeed = playerMovementStats.normalSpeed;
            currentMax = playerMovementStats.maxNormalSpeed;
        }
    }
    private void MovePlayer()
    {
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            Vector3 rawMoveDir = transform.right * movementInput.x + transform.forward * movementInput.y;
            rawMoveDir.Normalize();
            Vector3 moveDir = rawMoveDir;

            Vector2 currentVel = new Vector2(rb.velocity.x, rb.velocity.z);
            CounterMovement(movementInput, currentVel);

            if (isOnSlope && !isJumping)
            {
                Vector3 slopeDir = GetSlopeMoveDirection(moveDir);
                float slopeMultiplier = Mathf.Lerp(minSlopeYVelocity, maxSlopeYVelocity, Vector3.Angle(Vector3.up, slopeHit.normal) / maxSlopeAngle);
                rb.AddForce(slopeDir * currentSpeed * 5f * slopeMultiplier, ForceMode.Force);

                if (rb.velocity.y > maxSlopeYVelocity)
                {
                    rb.velocity = new Vector3(rb.velocity.x, maxSlopeYVelocity, rb.velocity.z).normalized;
                }
            }
            else if (!isJumping && wasOnSlope && !isOnSlope)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;
            }
            else
            {
                rb.AddForce(moveDir * currentSpeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            if (!isJumping && (isOnSlope || wasOnSlope))
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (rb.velocity.x != 0 || rb.velocity.z != 0)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }

    private void CounterMovement(Vector2 input, Vector2 currentVel)
    {
        if (Mathf.Abs(currentVel.x) > threshold && Mathf.Abs(input.x) < 0.05f)
        {
            rb.AddForce(transform.right * -currentVel.x * counterForce, ForceMode.Force);
        }

        if (Mathf.Abs(currentVel.y) > threshold && Mathf.Abs(input.y) < 0.05f)
        {
            rb.AddForce(transform.forward * -currentVel.y * counterForce, ForceMode.Force);
        }

        if ((currentVel.x < -threshold && input.x > 0) || (currentVel.x > threshold && input.x < 0))
        {
            rb.AddForce(transform.right * -currentVel.x * counterForce * 0.5f, ForceMode.Force);
        }

        if ((currentVel.y < -threshold && input.y > 0) || (currentVel.y > threshold && input.y < 0))
        {
            rb.AddForce(transform.forward * -currentVel.y * counterForce * 0.5f, ForceMode.Force);
        }
    }
    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > currentMax)
        {
            Vector3 limitedVel = flatVel.normalized * currentMax;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        if (isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * playerMovementStats.jumpForce, ForceMode.Impulse);
            jumpTimer += Time.deltaTime;
        }
    }

    private void Crouch()
    {
        Debug.DrawRay(bottomChild.transform.position, Vector3.up * detectionLenght, Color.red);
        if (playerStateHandler.GetState() == PlayerStates.CROUCHING || playerStateHandler.GetState() == PlayerStates.SLIDING)
        {
            topCollider.isTrigger = true;
        }
        else
        {
            topCollider.isTrigger = false;
        }
    }

    private void Fall()
    {
        if (rb.velocity.y < 0 && !isJumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (playerMovementStats.fallForce - 1) * Time.fixedDeltaTime;
        }
    }
    //Slope
    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection)
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    private bool IsSlope(out RaycastHit hitInfo)
    {
        Vector3 origin = base.transform.position + Vector3.up * 0.5f;
        Vector3 bottom = base.transform.position + Vector3.up * 0.1f;
        float castRadius = 0.4f;
        float castDistance = slopeDistance;

        if (Physics.CapsuleCast(origin, bottom, castRadius, Vector3.down, out hitInfo, castDistance, layerObstacleDetection))
        {
            float angle = Vector3.Angle(Vector3.up, hitInfo.normal);
            return angle > 0f && angle <= maxSlopeAngle;
        }
        return false;
    }
    private void UpdateSlopeStatus()
    {
        wasOnSlope = isOnSlope;
        isOnSlope = IsSlope(out slopeHit);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Vector3 origin = base.transform.position + Vector3.up * 0.5f;
        Vector3 bottom = base.transform.position + Vector3.up * 0.1f;
        float castDistance = slopeDistance;

        Vector3 direction = Vector3.down * castDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction);
        Gizmos.DrawLine(bottom, bottom + direction);
    }
}
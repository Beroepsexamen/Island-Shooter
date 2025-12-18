using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;

    private float moveSpeed;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDrag = 8f;
    private bool isGrounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle = 25f;
    private RaycastHit slopeHit;

    public Transform playerObj;

    private float horizontalInput;
    private float verticalInput;

    [Header("Jumping")]
    public float jumpCooldown = 0.5f;
    public float airMultiplier = 0.4f;
    public float jumpForce = 7f;
    private bool readyToJump = true;

    private Vector3 moveDirection;
    private Animator playerAnimator;

    private int xVelHash;
    private int yVelHash;

    private Rigidbody rb;


    private void Start()
    {
        // Get player components
        playerAnimator = playerObj.GetComponent<Animator>();

        xVelHash = Animator.StringToHash("XVelocity");
        yVelHash = Animator.StringToHash("YVelocity");

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Check if on ground
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);

        // Set move speed
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        MyInput();
        SpeedControl();
        AnimationControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if (Input.GetKey(KeyCode.Space) && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = playerObj.forward * verticalInput + playerObj.right * horizontalInput;

        // On slope
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y <= 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // On ground
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // In air
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // Turn off gravity while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // Limiting speed on slopes
        if (OnSlope())
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

        // Limit velocity on ground
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void AnimationControl()
    {
        // Get local velocity of the player
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        Vector3 localVel = playerObj.InverseTransformDirection(flatVel);

        // Animate player movement
        playerAnimator.SetFloat(xVelHash, localVel.x);
        playerAnimator.SetFloat(yVelHash, localVel.z);
    }

    private void Jump()
    {
        // Make the player jump
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool OnSlope()
    {
        // Check if player is on a slope
        if (Physics.Raycast(groundCheck.position, Vector3.down, out slopeHit, 0.2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        // Get the direction to move on the slope
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
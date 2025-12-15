using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float WalkSpeed;
    public float SprintSpeed;

    private float MoveSpeed;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public LayerMask Ground;
    public float GroundDrag;
    private bool IsGrounded;

    [Header("Slope Handling")]
    public float MaxSlopeAngle;
    private RaycastHit SlopeHit;

    public Transform PlayerObj;

    float HorizontalInput;
    float VerticalInput;

    [Header("Jumping")]
    public float JumpCooldown = 0.5f;
    public float AirMultiplier = 0.4f;
    public float JumpForce = 12f;
    private bool ReadyToJump = true;

    Vector3 MoveDirection;

    Transform Orientation;
    Animator Animator;

    private int XVelHash;
    private int YVelHash;

    Rigidbody RigidBody;


    private void Start()
    {
        Animator = PlayerObj.GetComponent<Animator>();
        Orientation = transform.Find("Orientation");

        XVelHash = Animator.StringToHash("XVelocity");
        YVelHash = Animator.StringToHash("YVelocity");

        RigidBody = GetComponent<Rigidbody>();
        RigidBody.freezeRotation = true;
    }

    private void Update()
    {
        // Check if on ground
        IsGrounded = Physics.Raycast(GroundCheck.position, Vector3.down, 0.1f, Ground);

        // Set move speed
        MoveSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : WalkSpeed;

        if (IsGrounded)
            RigidBody.linearDamping = GroundDrag;
        else
            RigidBody.linearDamping = 0;

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
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if (Input.GetKey(KeyCode.Space) && ReadyToJump && IsGrounded)
        {
            ReadyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), JumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        MoveDirection = PlayerObj.forward * VerticalInput + PlayerObj.right * HorizontalInput;

        // On slope
        if (OnSlope())
        {
            RigidBody.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);

            if (RigidBody.linearVelocity.y <= 0)
                RigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // On ground
        if (IsGrounded)
            RigidBody.AddForce(MoveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);

        // In air
        else if (!IsGrounded)
            RigidBody.AddForce(MoveDirection.normalized * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);

        // Turn off gravity while on slope
        RigidBody.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // Limiting speed on slopes
        if (OnSlope())
        {
            if (RigidBody.linearVelocity.magnitude > MoveSpeed)
                RigidBody.linearVelocity = RigidBody.linearVelocity.normalized * MoveSpeed;
        }

        // Limit velocity on ground
        else
        {
            Vector3 FlatVel = new Vector3(RigidBody.linearVelocity.x, 0f, RigidBody.linearVelocity.z);

            if (FlatVel.magnitude > MoveSpeed)
            {
                Vector3 LimitedVel = FlatVel.normalized * MoveSpeed;
                RigidBody.linearVelocity = new Vector3(LimitedVel.x, RigidBody.linearVelocity.y, LimitedVel.z);
            }
        }
    }

    private void AnimationControl()
    {
        Vector3 FlatVel = new Vector3(RigidBody.linearVelocity.x, 0f, RigidBody.linearVelocity.z);
        Vector3 LocalVel = PlayerObj.InverseTransformDirection(FlatVel);

        Animator.SetFloat(XVelHash, LocalVel.x);
        Animator.SetFloat(YVelHash, LocalVel.z);
    }

    private void Jump()
    {
        RigidBody.linearVelocity = new Vector3(RigidBody.linearVelocity.x, 0f, RigidBody.linearVelocity.z);
        RigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        ReadyToJump = true;
    }

    private bool OnSlope()
    {
        // Check if player is on a slope
        if (Physics.Raycast(GroundCheck.position, Vector3.down, out SlopeHit, 0.2f))
        {
            float Angle = Vector3.Angle(Vector3.up, SlopeHit.normal);
            return Angle < MaxSlopeAngle && Angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        // Get the direction to move on the slope
        return Vector3.ProjectOnPlane(MoveDirection, SlopeHit.normal).normalized;
    }
}
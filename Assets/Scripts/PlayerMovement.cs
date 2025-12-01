using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;

    [HideInInspector] public float WalkSpeed;
    [HideInInspector] public float SprintSpeed;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask Ground;
    public float GroundDrag;
    private bool Grounded;

    [Header("Slope Handling")]
    public float MaxSlopeAngle;
    private RaycastHit SlopeHit;

    public Transform Orientation;

    float HorizontalInput;
    float VerticalInput;

    [Header("Jumping")]
    public float JumpCooldown = 0.5f;
    public float AirMultiplier = 0.2f;
    public float JumpForce = 8f;
    private bool ReadyToJump = true;

    Vector3 MoveDirection;

    Rigidbody RigidBody;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.freezeRotation = true;
    }

    private void Update()
    {
        // Check if on ground
        Grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight + 0.1f, Ground);

        if (Grounded)
            RigidBody.linearDamping = GroundDrag;
        else
            RigidBody.linearDamping = 0;

        MyInput();
        SpeedControl();
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
        if (Input.GetKey(KeyCode.Space) && ReadyToJump && Grounded)
        {
            ReadyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), JumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        // On slope
        if (OnSlope())
        {
            RigidBody.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);

            if (RigidBody.linearVelocity.y <= 0)
                RigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // On ground
        if (Grounded)
            RigidBody.AddForce(MoveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);

        // In air
        else if (!Grounded)
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
                Vector3 limitedVel = FlatVel.normalized * MoveSpeed;
                RigidBody.linearVelocity = new Vector3(limitedVel.x, RigidBody.linearVelocity.y, limitedVel.z);
            }
        }
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
        if (Physics.Raycast(transform.position, Vector3.down, out SlopeHit, PlayerHeight * 0.5f + 0.2f))
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
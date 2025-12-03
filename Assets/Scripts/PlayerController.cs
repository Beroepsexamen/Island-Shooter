using UnityEngine;
using IslandShooter.Manager;

namespace IslandShooter.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float LowerLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;

        private Rigidbody PlayerRigidbody;
        private InputManager InputManager;
        private Animator PlayerAnimator;
        private bool HasAnimator;
        private int XVelHash;
        private int YVelHash;
        private float XRotation;

        private const float WalkSpeed = 5f;

        private Vector2 CurrentVelocity;

        private void Start()
        {
            HasAnimator = TryGetComponent<Animator>(out PlayerAnimator);
            PlayerRigidbody = GetComponent<Rigidbody>();
            InputManager = GetComponent<InputManager>();

            XVelHash = Animator.StringToHash("XVelocity");
            YVelHash = Animator.StringToHash("YVelocity");
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!HasAnimator) return;

            float TargetSpeed = WalkSpeed;
            if (InputManager.Move == Vector2.zero) TargetSpeed = 0.1f;

            CurrentVelocity.x = Mathf.Lerp(CurrentVelocity.x, InputManager.Move.x * TargetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            CurrentVelocity.y = Mathf.Lerp(CurrentVelocity.y, InputManager.Move.y * TargetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            var XVelDifference = CurrentVelocity.x - PlayerRigidbody.linearVelocity.x;
            var ZVelDifference = CurrentVelocity.y - PlayerRigidbody.linearVelocity.z;

            PlayerRigidbody.AddForce(transform.TransformVector(new Vector3(XVelDifference, 0, ZVelDifference)), ForceMode.VelocityChange);

            PlayerAnimator.SetFloat(XVelHash, CurrentVelocity.x);
            PlayerAnimator.SetFloat(YVelHash, CurrentVelocity.y);
        }

        private void CamMovements()
        {
            if (!HasAnimator) return;

            var Mouse_X = InputManager.Look.x;
            var Mouse_Y = InputManager.Look.y;
            Camera.position = CameraRoot.position;

            XRotation -= Mouse_Y * MouseSensitivity * Time.deltaTime;
            XRotation = Mathf.Clamp(XRotation, UpperLimit, LowerLimit);

            Camera.localRotation = Quaternion.Euler(XRotation, 0, 0);
            transform.Rotate(Vector3.up, Mouse_X * MouseSensitivity * Time.deltaTime);
        }
    }
}

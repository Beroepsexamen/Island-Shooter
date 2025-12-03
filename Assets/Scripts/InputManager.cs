using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace IslandShooter.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        private InputActionMap CurrentMap;
        private InputAction MoveAction;
        private InputAction LookAction;

        private void Awake()
        {
            CurrentMap = PlayerInput.currentActionMap;
            MoveAction = CurrentMap.FindAction("Move");
            LookAction = CurrentMap.FindAction("Look");

            MoveAction.performed += OnMove;
            LookAction.performed += OnLook;

            MoveAction.canceled += OnMove;
            LookAction.canceled += OnLook;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            CurrentMap.Enable();
        }

        private void OnDisable()
        {
            CurrentMap.Disable();
        }
    }
}
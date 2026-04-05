using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Player
{
    public class PlayerInputProvider : MonoBehaviour
    {
        [Header("State")] public bool IsLocked = false;

        [Header("Input References")] [SerializeField]
        private InputActionReference _moveAction;

        [SerializeField] private InputActionReference _interactAction;
        [SerializeField] private InputActionReference _inventoryAction;
        [SerializeField] private InputActionReference _attackAction;
        [Header("Mouse Settings")]
        [SerializeField] private InputActionReference _mousePositionAction;

        public Vector2 MousePosition { get; private set; }
        public Vector2 MoveInput { get; private set; }

        public event Action OnInteract;
        public event Action OnToggleInventory;
        public event Action OnAttack;

        private void OnEnable()
        {
            if (_moveAction != null) _moveAction.action.Enable();

            if (_interactAction != null)
            {
                _interactAction.action.Enable();
                _interactAction.action.performed += PerformInteract;
            }

            if (_inventoryAction != null)
            {
                _inventoryAction.action.Enable();
                _inventoryAction.action.performed += PerformInventoryToggle;
            }

            if (_attackAction != null)
            {
                _attackAction.action.Enable();
                _attackAction.action.performed += PerformAttack;
            }
            
            _mousePositionAction.action.Enable();
        }

        private void OnDisable()
        {
            if (_moveAction != null) _moveAction.action.Disable();

            if (_interactAction != null)
            {
                _interactAction.action.Disable();
                _interactAction.action.performed -= PerformInteract;
            }

            if (_inventoryAction != null)
            {
                _inventoryAction.action.Disable();
                _inventoryAction.action.performed -= PerformInventoryToggle;
            }

            if (_attackAction != null)
            {
                _attackAction.action.Disable();
                _attackAction.action.performed -= PerformAttack;
            }
            
            _mousePositionAction.action.Disable();
        }

        private void Update()
        {
            if (_mousePositionAction != null)
            {
                MousePosition = _mousePositionAction.action.ReadValue<Vector2>();
            }
            
            if (IsLocked)
            {
                MoveInput = Vector2.zero;
                return;
            }
            
            if (_moveAction != null)
            {
                MoveInput = _moveAction.action.ReadValue<Vector2>();
            }
        }

        private void PerformInteract(InputAction.CallbackContext context)
        {
            if (IsLocked) return;
            OnInteract?.Invoke();
        }

        private void PerformInventoryToggle(InputAction.CallbackContext context)
        {
            OnToggleInventory?.Invoke();
        }

        private void PerformAttack(InputAction.CallbackContext context)
        {
            if (IsLocked) return;
            OnAttack?.Invoke();
        }
    }
}
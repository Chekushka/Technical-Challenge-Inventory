using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInputProvider))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 6f;
        [SerializeField] private float _rotationSpeed = 10f;

        private Rigidbody _rb;
        private PlayerInputProvider _input;
        private Transform _mainCameraTransform;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInputProvider>();
            
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        
            if (Camera.main != null)
            {
                _mainCameraTransform = Camera.main.transform;
            }
        }

        private void FixedUpdate()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            Vector2 inputVector = _input.MoveInput;
            
            if (inputVector.sqrMagnitude < 0.01f)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
                return;
            }
            
            Vector3 camForward = _mainCameraTransform.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 camRight = _mainCameraTransform.right;
            camRight.y = 0;
            camRight.Normalize();
            
            Vector3 moveDirection = (camForward * inputVector.y + camRight * inputVector.x).normalized;
            _rb.linearVelocity = new Vector3(moveDirection.x * _moveSpeed, _rb.linearVelocity.y, moveDirection.z * _moveSpeed);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
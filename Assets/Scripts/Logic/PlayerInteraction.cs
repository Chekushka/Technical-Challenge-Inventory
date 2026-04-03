using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _interactionRadius = 3f;
        [SerializeField] private LayerMask _interactableLayer;
    
        [Header("Input")]
        [SerializeField] private InputActionReference _interactAction;

        private IInteractable _currentTarget;
        
        private void OnEnable()
        {
            if (_interactAction != null)
            {
                _interactAction.action.performed += OnInteractPerformed;
                _interactAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (_interactAction != null)
            {
                _interactAction.action.performed -= OnInteractPerformed;
                _interactAction.action.Disable();
            }
        }

        private void Update()
        {
            FindClosestInteractable();
        }
        
        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            if (_currentTarget != null)
            {
                _currentTarget.Interact();
                _currentTarget = null;
            }
        }

        private void FindClosestInteractable()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius, _interactableLayer);
        
            IInteractable closestInteractable = null;
            float closestDistance = float.MaxValue;

            foreach (var col in colliders)
            {
                if (col.TryGetComponent(out IInteractable interactable))
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = interactable;
                    }
                }
            }

            if (closestInteractable != _currentTarget)
            {
                _currentTarget?.OnHoverExit(); 
                _currentTarget = closestInteractable;
                _currentTarget?.OnHoverEnter(); 
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
    }
}
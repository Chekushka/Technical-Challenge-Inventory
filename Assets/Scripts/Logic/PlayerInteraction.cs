using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic
{
    [RequireComponent(typeof(PlayerInputProvider))]
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _interactionRadius = 3f;
        [SerializeField] private GameObject _interactableUI;
        [SerializeField] private LayerMask _interactableLayer;

        private PlayerInputProvider _input;
        private IInteractable _currentTarget;
        private PlayerAnimator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();
            _input = GetComponent<PlayerInputProvider>();
        }
        
        private void OnEnable() => _input.OnInteract += HandleInteraction;

        private void OnDisable() =>  _input.OnInteract -= HandleInteraction;
        
        private void HandleInteraction()
        {
            if (_currentTarget != null)
            {
                _animator.PlayPickUp();
                _input.IsLocked = true;
                
                _animator.OnAnimationImpact = () => {
                    _currentTarget.Interact();
                    _currentTarget = null;
                    _input.IsLocked = false;
                };
            }
        }

        private void Update()
        {
            FindClosestInteractable();
        }

        private void FindClosestInteractable()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius, _interactableLayer);
            
            _interactableUI.SetActive(colliders.Length > 0);
        
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
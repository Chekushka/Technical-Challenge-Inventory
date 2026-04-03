using UnityEngine;

namespace Logic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerInputProvider _input;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private float _hitRadius = 1.5f;
        [SerializeField] private LayerMask _boxLayer;

        private void OnEnable() => _input.OnAttack += TryBreak;
        private void OnDisable() => _input.OnAttack -= TryBreak;

        private void TryBreak()
        {
            _animator.PlayAttack();
            
            Vector3 checkPos = transform.position + transform.forward * 0.5f;
            Collider[] hitColliders = Physics.OverlapSphere(checkPos, _hitRadius, _boxLayer);

            foreach (var col in hitColliders)
            {
                if (col.TryGetComponent(out BreakableObject obj))
                {
                    obj.Break();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + transform.forward * 0.5f, _hitRadius);
        }
    }
}
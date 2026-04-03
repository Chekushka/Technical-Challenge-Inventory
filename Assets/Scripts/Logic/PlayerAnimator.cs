using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator :  MonoBehaviour
    {
        [SerializeField] private PlayerInputProvider _input;
        [SerializeField] private float _dampTime = 0.1f;

        private Animator _animator;
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int PickUpHash = Animator.StringToHash("PickUp");
        
        public Action OnAnimationImpact;
        
        public void PlayAttack() => _animator.SetTrigger(AttackHash);
        public void PlayPickUp() => _animator.SetTrigger(PickUpHash);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
       
        private void Update()
        {
            float currentSpeed = _input.MoveInput.sqrMagnitude;
            _animator.SetFloat(SpeedHash, currentSpeed, _dampTime, Time.deltaTime);
        }
        
        public void ExecuteInteraction()
        {
            OnAnimationImpact?.Invoke();
        }
    }
}
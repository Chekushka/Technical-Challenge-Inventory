using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class HintButtonsController : MonoBehaviour
    {
        [SerializeField] private Image _hudInventoryIcon;
        [SerializeField] private GameObject _hudInteractButton;
        
        [Header("Settings")]
        [SerializeField] private float _shakeDuration = 0.5f;
        [SerializeField] private float _shakeStrength = 10f;
        [SerializeField] private float _punchScaleAmount = 0.2f;
        [SerializeField] private Color _fullColor = Color.red;
        
        private RectTransform _rectTransform;
        private Color _originalColor;
        private Vector3 _originalScale;
        private Tween _currentTween;

        public static HintButtonsController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            _rectTransform = _hudInventoryIcon.rectTransform;
            _originalColor = _hudInventoryIcon.color;
            _originalScale = _rectTransform.localScale;
        }

        public void EnableInteractHint()
        {
            _hudInteractButton.SetActive(true);
        }

        public void DisableInteractHint()
        {
            _hudInteractButton.SetActive(false);
        }

        public void ShowInventoryFullWarning()
        {
           PlayFullInventoryAnimation();
        }

        private void PlayFullInventoryAnimation()
        {
            _currentTween?.Kill(true); 
            _rectTransform.localScale = _originalScale;
            _hudInventoryIcon.color = _originalColor;
            
            Sequence warningSequence = DOTween.Sequence();

            warningSequence.Append(
                _rectTransform.DOShakeAnchorPos(_shakeDuration, new Vector2(_shakeStrength, 0), 10, 90, false, true)
            );

            warningSequence.Join(
                _rectTransform.DOPunchScale(new Vector3(_punchScaleAmount, _punchScaleAmount, 0), _shakeDuration, 5, 1)
            );

            warningSequence.Join(
                _hudInventoryIcon.DOColor(_fullColor, _shakeDuration / 2).SetLoops(2, LoopType.Yoyo)
            );

            _currentTween = warningSequence;
        }
    }
}
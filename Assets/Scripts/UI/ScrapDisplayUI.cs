using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScrapDisplayUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _scrapText;

        [Header("Settings")]
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Vector3 _punchScale = new Vector3(1.2f, 1.2f, 1.2f);

        private int _currentValue = 0;

        private void OnDisable()
        {
            if (InventoryController.Instance != null)
                InventoryController.Instance.OnScrapTotalChanged -= UpdateDisplay;
        }

        private void Start()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.OnScrapTotalChanged += UpdateDisplay;
            }
            
            _currentValue = InventoryController.Instance.TotalScrap;
            _scrapText.text = $"{_currentValue}";
        }

        private void UpdateDisplay(int newValue)
        {
            _scrapText.transform.DOPunchScale(_punchScale, _animationDuration, 5, 1);
        
            DOTween.To(() => _currentValue, x => _currentValue = x, newValue, _animationDuration)
                .OnUpdate(() =>
                {
                    _scrapText.text = $"{_currentValue}";
                })
                .SetEase(Ease.OutQuad);
        }
    }
}
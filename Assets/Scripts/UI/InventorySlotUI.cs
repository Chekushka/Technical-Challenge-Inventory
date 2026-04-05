using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI References")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scrapAmountText;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _maskImage;
        [SerializeField] private GameObject _checkmarkImage;
        
        [Header("Empty State")]
        [SerializeField] private Sprite _emptySlotSprite;

        private ItemInstance _currentItem;
        private bool _isEmpty = true;

        public void SetupFull(ItemInstance item, bool isSelected)
        {
            _currentItem = item;
            _isEmpty = false;
            _maskImage.gameObject.SetActive(false);
        
            _iconImage.gameObject.SetActive(true);
            _nameText.gameObject.SetActive(true);
            _scrapAmountText.gameObject.SetActive(true);
            _iconImage.sprite = item.data.icon;
            _nameText.text = item.GetDisplayName();
            _nameText.color = item.GetColor();
            _frameImage.color = item.GetColor();
            _scrapAmountText.text = $"{item.GetTotalValue()}";
            _checkmarkImage.SetActive(isSelected);
        }
        
        public void SetupEmpty()
        {
            _currentItem = null;
            _isEmpty = true;
            _maskImage.gameObject.SetActive(false);
        
            _iconImage.gameObject.SetActive(true);
            _iconImage.sprite = _emptySlotSprite; 
            _frameImage.color = new Color(1, 0.94f, 0.82f, 1);
        
            if (_nameText != null) _nameText.gameObject.SetActive(false);
            if (_scrapAmountText != null) _scrapAmountText.gameObject.SetActive(false);
            _checkmarkImage.SetActive(false);
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isEmpty) return;
            
            if (InventoryController.Instance.IsScrapMode)
            {
                InventoryController.Instance.ToggleScrapSelection(_currentItem);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isEmpty && !InventoryController.Instance.IsScrapMode)
            {
                _maskImage.gameObject.SetActive(true);
                ItemTooltipUI.Instance.Show(_currentItem);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _maskImage.gameObject.SetActive(false);
            if (!_isEmpty) ItemTooltipUI.Instance.Hide();
        }
    }
}
using Data;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scrapAmountText;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Button _scrapButton;

        private ItemInstance _currentItem;

        public void Setup(ItemInstance item)
        {
            _currentItem = item;
        
            _iconImage.sprite = item.data.icon;
            _nameText.text = item.GetDisplayName();
            _nameText.color = item.GetColor(); 
            _scrapAmountText.text = $"+{item.GetTotalValue()}";

            _scrapButton.onClick.RemoveAllListeners();
            _scrapButton.onClick.AddListener(OnScrapClicked);
        }

        private void OnScrapClicked()
        {
            InventoryController.Instance.ScrapItem(_currentItem);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemTooltipUI.Instance.Show(_currentItem);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            ItemTooltipUI.Instance.Hide();
        }
    }
}
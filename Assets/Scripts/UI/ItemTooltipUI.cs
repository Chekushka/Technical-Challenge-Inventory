using Data;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemTooltipUI : MonoBehaviour
    {
        public static ItemTooltipUI Instance { get; private set; }

        [Header("UI Elements")]
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _categoryText;
        [SerializeField] private TextMeshProUGUI _rarityText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private PlayerInputProvider _input;

        private void Awake()
        {
            Instance = this;
            Hide();
        }

        public void Show(ItemInstance item)
        {
            _panel.SetActive(true);

            _iconImage.sprite = item.data.icon;
            _nameText.text = item.GetDisplayName();
            _nameText.color = item.GetColor();

            _categoryText.text = $"Category: {item.data.itemCategory}";
            _rarityText.text = $"Rarity: {item.rarity}";
            _rarityText.color = item.GetColor();
        
            _descriptionText.text = item.data.description;
            _valueText.text = $"Scrap Value: {item.GetTotalValue()}";
        
            // Можна додати логіку, щоб вікно слідувало за мишкою
            // transform.position = Input.mousePosition;
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }

        private void Update()
        {
            if (_panel.activeSelf)
            {
                Vector3 offset = new Vector3(15, -15, 0);
                transform.position = Input.mousePosition + offset;
            }
        }
    }
}
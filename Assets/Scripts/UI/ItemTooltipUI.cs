using Data;
using Logic;
using Logic.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ItemTooltipUI : MonoBehaviour
    {
        public static ItemTooltipUI Instance { get; private set; }

        [Header("UI Elements")]
        [SerializeField] private GameObject _panel;
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
            
            _nameText.text = item.GetDisplayName();
            _nameText.color = item.GetColor();

            _categoryText.text = $"Category: {item.data.itemCategory}";
            _rarityText.text = $"Rarity: {item.rarity.rarityType}";
            _rarityText.color = item.GetColor();
        
            _descriptionText.text = item.data.description;
            _valueText.text = $"Scrap Value: {item.GetTotalValue()}";
            Vector2 offset = new Vector2(200, -200);
            _panel.transform.position = _input.MousePosition + offset;
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }
    }
}
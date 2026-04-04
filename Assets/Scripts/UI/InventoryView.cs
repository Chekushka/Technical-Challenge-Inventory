using Logic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI _slotPrefab;
        [SerializeField] private Transform _contentArea;
        [SerializeField] private TextMeshProUGUI _scrapCounterText;

        private void OnEnable()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.OnInventoryUpdated += RefreshUI;
                RefreshUI();
            }
        }

        private void OnDisable()
        {
            if (InventoryController.Instance != null)
                InventoryController.Instance.OnInventoryUpdated -= RefreshUI;
        }

        public void RefreshUI()
        {
            foreach (Transform child in _contentArea)
            {
                Destroy(child.gameObject);
            }
            
            var data = InventoryController.Instance.GetData();
            foreach (var item in data.items)
            {
                InventorySlotUI slotObj = Instantiate(_slotPrefab, _contentArea);
                slotObj.Setup(item);
            }
            
            _scrapCounterText.text = $"Scrap: {data.currentScrap}";
        }
    }
}
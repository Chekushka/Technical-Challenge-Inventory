using Logic;
using UnityEngine;

namespace UI
{
    public class InventoryToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private PlayerInputProvider _input;

        private void OnEnable() => _input.OnToggleInventory += Toggle;
        private void OnDisable() => _input.OnToggleInventory -= Toggle;

        public void Toggle()
        {
            bool isActive = !_inventoryPanel.activeSelf;
            _inventoryPanel.SetActive(isActive);
            
            _input.IsLocked = isActive;
            
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;
        }
    }
}
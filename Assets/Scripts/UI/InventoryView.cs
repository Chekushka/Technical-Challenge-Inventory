using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Logic;
using Logic.Player;
using UI;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public static InventoryView Instance { get; private set; }

    [Header("Main Inventory")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _contentArea;
    [SerializeField] private PlayerInputProvider _input;
    
    [Header("Scrap Mode Panel")]
    [SerializeField] private GameObject _bottomScrapPanel;
    [SerializeField] private TextMeshProUGUI _potentialScrapText;

    private List<InventorySlotUI> _instantiatedSlots = new List<InventorySlotUI>();

    private void Awake() => Instance = this;
    
    private void OnEnable() => _input.OnToggleInventory += Toggle;
    private void OnDisable() => _input.OnToggleInventory -= Toggle;

    private void Start()
    {
        int capacity = InventoryController.Instance.GetCapacity();
        for (int i = 0; i < capacity; i++)
        {
            GameObject slotObj = Instantiate(_slotPrefab, _contentArea);
            _instantiatedSlots.Add(slotObj.GetComponent<InventorySlotUI>());
        }
        
        InventoryController.Instance.OnInventoryUpdated += RefreshUI;
        RefreshUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RefreshUI()
    {
        var items = InventoryController.Instance.GetItems();
        var selected = InventoryController.Instance.SelectedForScrap;

        for (int i = 0; i < _instantiatedSlots.Count; i++)
        {
            if (i < items.Count)
            {
                bool isSelected = selected.Contains(items[i]);
                _instantiatedSlots[i].SetupFull(items[i], isSelected);
            }
            else
            {
                _instantiatedSlots[i].SetupEmpty();
            }
        }

        UpdateScrapPanelUI();
    }

    private void UpdateScrapPanelUI()
    {
        if (!InventoryController.Instance.IsScrapMode) return;

        int totalValue = 0;
        foreach (var item in InventoryController.Instance.SelectedForScrap)
        {
            totalValue += item.GetTotalValue();
        }

        _potentialScrapText.text = $"Gain: {totalValue} Scrap";
    }
    
    public void Toggle()
    {
        bool isActive = !_inventoryPanel.activeSelf;

        _inventoryPanel.SetActive(isActive);
            
        _input.IsLocked = isActive;
            
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
        
        if(isActive)
            RefreshUI();
        else
        {
            InventoryController.Instance.IsScrapMode = false;
            InventoryController.Instance.SelectedForScrap.Clear();
            _bottomScrapPanel.SetActive(false);
            ItemTooltipUI.Instance.Hide();
        }
    }
    
    public void OpenInScrapMode()
    {
        InventoryController.Instance.IsScrapMode = true;
        _bottomScrapPanel.SetActive(true);
        _inventoryPanel.SetActive(true);
        RefreshUI();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _input.IsLocked = true;
    }
    
    public void CloseInventory()
    {
        InventoryController.Instance.IsScrapMode = false;
        InventoryController.Instance.SelectedForScrap.Clear();
        _bottomScrapPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _input.IsLocked = false;
    }
}
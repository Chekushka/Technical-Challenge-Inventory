using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryData _inventoryData;
    public static InventoryController Instance { get; private set; }
    
    public HashSet<ItemInstance> SelectedForScrap { get; private set; } = new HashSet<ItemInstance>();
    public bool IsScrapMode { get; set; } = false;
    public int TotalScrap { get; private set; }
    
    public event Action<int> OnScrapTotalChanged;
    public event Action OnInventoryUpdated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        TotalScrap = _inventoryData.currentScrap;
    }

    public int GetCapacity() => _inventoryData.capacity;
    
    public void IncreaseCapacity(int amount) => _inventoryData.capacity += amount;
    
    public List<ItemInstance> GetItems() => _inventoryData.items;
    
    public bool TryAddItem(ItemInstance item)
    {
        if (item == null) return false;
        if (_inventoryData.items.Contains(item))
            return false;
        
        if (_inventoryData.items.Count >= _inventoryData.capacity)
        {
            return false;
        }

        _inventoryData.items.Add(item);
        OnInventoryUpdated?.Invoke();
        return true;
    }

    public void ToggleScrapSelection(ItemInstance item)
    {
        if (!IsScrapMode || item == null) return;
        
        if (!_inventoryData.items.Contains(item))
            return;
        
        if (SelectedForScrap.Contains(item))
            SelectedForScrap.Remove(item);
        else
            SelectedForScrap.Add(item);

        OnInventoryUpdated?.Invoke();
    }

    public void ConfirmScrap()
    {
        int totalScrapGained = 0;
        foreach (var item in SelectedForScrap)
        {
            totalScrapGained += item.GetTotalValue();
            _inventoryData.items.Remove(item);
        }

        _inventoryData.currentScrap += totalScrapGained;
        TotalScrap = _inventoryData.currentScrap;

        SelectedForScrap.Clear();
        OnInventoryUpdated?.Invoke();
        OnScrapTotalChanged?.Invoke(TotalScrap);
    }
}
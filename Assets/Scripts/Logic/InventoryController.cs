using System;
using Data;
using UnityEngine;

namespace Logic
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryData _data;
        public static InventoryController Instance { get; private set; }
        public event Action OnInventoryUpdated;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void AddItem(ItemInstance item)
        {
            if(item == null) return;
            _data.items.Add(item);
            
            OnInventoryUpdated?.Invoke();
        }

        public void ScrapItem(ItemInstance item)
        {
            if(!_data.items.Contains(item)) return;

            int value = item.GetPrice();
            _data.currentScrap += value;
            OnInventoryUpdated?.Invoke();
            Debug.Log($"Scrapped {item.GetDisplayName()} for {value} dust.");
        }

        public InventoryData GetData() => _data;
    }
}
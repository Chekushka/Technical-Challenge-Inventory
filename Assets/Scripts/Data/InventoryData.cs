using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/Inventory Data")]
    public class InventoryData : ScriptableObject
    {
        public List<ItemInstance> items = new List<ItemInstance>();
        public int currentScrap = 0;
        public int capacity = 20;
    }
}
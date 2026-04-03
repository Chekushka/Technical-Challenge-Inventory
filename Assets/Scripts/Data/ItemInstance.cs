using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemInstance
    {
        public BaseItemData itemData;
        public ItemModifier modifier;

        public ItemInstance(BaseItemData itemData, ItemModifier modifier = null)
        {
            this.itemData = itemData;
            this.modifier = modifier;
        }
        
        public Color GetDisplayColor() 
        {
            return modifier != null ? modifier.GetColor() : Color.white;
        }

        public string GetDisplayName() =>
            modifier != null ? $"{modifier.prefix} {itemData.itemName}" : itemData.itemName;

        public int GetPrice()
        {
            return modifier != null ? modifier.ModifyValue(itemData.baseValue) : itemData.baseValue;
        }
    }
}
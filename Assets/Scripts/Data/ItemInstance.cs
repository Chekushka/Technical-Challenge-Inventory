using System;
using Data.Modifiers;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemInstance
    {
        public BaseItemData data;
        public ItemModifier modifier;
        public RaritySettings.RarityConfig rarity;
        
        public string UniqueId { get; private set; }

        public ItemInstance(BaseItemData data, ItemModifier modifier = null,
            RaritySettings.RarityConfig rarity = default)
        {
            this.data = data;
            this.modifier = modifier;
            this.rarity = rarity;
            
            UniqueId = Guid.NewGuid().ToString();
        }
        
        public override bool Equals(object obj)
        {
            if (obj is ItemInstance other)
            {
                return UniqueId == other.UniqueId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }

        public Color GetColor()
        {
            return modifier != null ? rarity.color : Color.white;
        }

        public string GetDisplayName()
        {
            string rarityPrefix = $"{rarity.rarityType} ";
            string modPrefix = modifier != null ? $"{modifier.prefix} " : "";
            return $"{rarityPrefix}{modPrefix}{data.itemName}";
        }

        public int GetPrice()
        {
            return modifier != null ? modifier.ModifyValue(data.baseValue) : data.baseValue;
        }

        public int GetTotalValue()
        {
            float rarityMult = rarity.valueMultiplier;
            int value = Mathf.RoundToInt(data.baseValue * rarityMult);

            if (modifier != null)
            {
                value = modifier.ModifyValue(value);
            }

            return value;
        }
    }
}
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemInstance
    {
        public BaseItemData itemData;
        public ItemModifier modifier;
        public RaritySettings.RarityConfig rarity;

        public ItemInstance(BaseItemData itemData, ItemModifier modifier = null,
            RaritySettings.RarityConfig rarity = default)
        {
            this.itemData = itemData;
            this.modifier = modifier;
            this.rarity = rarity;
        }

        public Color GetColor()
        {
            return modifier != null ? rarity.color : Color.white;
        }

        public string GetDisplayName()
        {
            string rarityPrefix = $"{rarity.rarityType} ";
            string modPrefix = modifier != null ? $"{modifier.prefix} " : "";
            return $"{rarityPrefix}{modPrefix}{itemData.itemName}";
        }

        public int GetPrice()
        {
            return modifier != null ? modifier.ModifyValue(itemData.baseValue) : itemData.baseValue;
        }

        public int GetTotalValue()
        {
            float rarityMult = rarity.valueMultiplier;
            int value = Mathf.RoundToInt(itemData.baseValue * rarityMult);

            if (modifier != null)
            {
                value = modifier.ModifyValue(value);
            }

            return value;
        }
    }
}
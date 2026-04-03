using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Inventory/Modifier")]
    public abstract class ItemModifier : ScriptableObject
    {
        public string prefix;
        public ItemRarity rarity;
        
        [SerializeField] protected RaritySettings raritySettings;

        public Color GetColor()
        {
            if (raritySettings == null) return Color.white;
            return raritySettings.GetConfig(rarity).color;
        }

        public int ModifyValue(int baseValue) 
        {
            if (raritySettings == null) return baseValue;
            float multiplier = raritySettings.GetConfig(rarity).valueMultiplier;
            return Mathf.RoundToInt(baseValue * multiplier);
        }
    }
}
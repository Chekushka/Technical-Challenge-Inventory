using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RaritySettings", menuName = "Inventory/Rarity Settings")]
    public class RaritySettings : ScriptableObject
    {
        [System.Serializable]
        public struct RarityConfig
        {
            public ItemRarity rarityType;
            public Color color;
            public float valueMultiplier;
        }

        public RarityConfig[] configs;
        
        public RarityConfig GetConfig(ItemRarity rarity)
        {
            return configs.FirstOrDefault(c => c.rarityType == rarity);
        }
    }
}
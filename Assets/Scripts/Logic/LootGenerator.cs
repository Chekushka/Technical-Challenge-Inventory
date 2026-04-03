using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Logic
{
    public class LootGenerator : MonoBehaviour
    {
        [SerializeField] private List<BaseItemData> _allPossibleItems;
        [SerializeField] private List<ItemModifier> _allPossibleModifiers;
        [SerializeField] private RaritySettings raritySettings;
        
        public static LootGenerator Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public ItemInstance GenerateRandomLoot()
        {
            BaseItemData randomBase = _allPossibleItems[Random.Range(0, _allPossibleItems.Count)];
            RaritySettings.RarityConfig randomRarity = raritySettings.GetConfig((ItemRarity)Random.Range(0, System.Enum.GetValues(typeof(ItemRarity)).Length));
            ItemModifier randomModifier = null;
            if (Random.value > 0.5f && _allPossibleModifiers.Count > 0)
            {
                randomModifier = _allPossibleModifiers[Random.Range(0, _allPossibleModifiers.Count)];
            }

            return new ItemInstance(randomBase, randomModifier, randomRarity);
        }
    }
}
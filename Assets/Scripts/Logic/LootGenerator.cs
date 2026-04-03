using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Logic
{
    public class LootGenerator : MonoBehaviour
    {
        [SerializeField] private List<BaseItemData> _allPossibleItems;
        [SerializeField] private List<ItemModifier> _allPossibleModifiers;

        public ItemInstance GenerateRandomLoot()
        {
            BaseItemData randomBase = _allPossibleItems[Random.Range(0, _allPossibleItems.Count)];
            
            ItemModifier randomModifier = null;
            if (Random.value > 0.5f && _allPossibleModifiers.Count > 0)
            {
                randomModifier = _allPossibleModifiers[Random.Range(0, _allPossibleModifiers.Count)];
            }

            return new ItemInstance(randomBase, randomModifier);
        }
    }
}
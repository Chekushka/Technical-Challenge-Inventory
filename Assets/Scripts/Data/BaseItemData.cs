using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
    public class BaseItemData : ScriptableObject
    {
        [Header("Visuals")]
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;
    
        [Header("Settings")]
        public int baseValue = 10;
        public ItemCategory itemCategory;
    }
}
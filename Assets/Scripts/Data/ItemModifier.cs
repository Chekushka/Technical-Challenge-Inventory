using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Inventory/Modifier")]
    public abstract class ItemModifier : ScriptableObject
    {
        public string prefix;

        public abstract int ModifyValue(int currentValue);
    }
}
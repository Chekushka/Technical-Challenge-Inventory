using UnityEngine;

namespace Data.Modifiers
{
    public abstract class ItemModifier : ScriptableObject
    {
        public string prefix;
        public Vector2Int modifierValueRange;

        public abstract int ModifyValue(int currentValue);
    }
}
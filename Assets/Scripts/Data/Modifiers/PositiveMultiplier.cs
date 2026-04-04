using UnityEngine;

namespace Data.Modifiers
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Inventory/Modifier/Positive")]
    public class PositiveMultiplier : ItemModifier
    {
        public override int ModifyValue(int currentValue)
        {
            return Mathf.RoundToInt(currentValue + Random.Range(modifierValueRange.x, modifierValueRange.y));
        }
    }
}
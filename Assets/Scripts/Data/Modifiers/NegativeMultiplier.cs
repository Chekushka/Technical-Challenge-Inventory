using UnityEngine;

namespace Data.Modifiers
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Inventory/Modifier/Negative")]
    public class NegativeMultiplier : ItemModifier
    {
        public override int ModifyValue(int currentValue)
        {
            var returnValue = Mathf.RoundToInt(currentValue - Random.Range(modifierValueRange.x, modifierValueRange.y));
            return returnValue > 0 ? returnValue : 0;
        }
    }
}
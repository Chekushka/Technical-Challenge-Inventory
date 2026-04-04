using UnityEngine;

namespace Data.Modifiers
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Inventory/Modifier/Neutral")]
    public class NeutralMultiplier : ItemModifier
    {
        public override int ModifyValue(int currentValue)
        {
            var returnValue = Random.Range(0, 1f) > 0.5f ? currentValue + -Random.Range(modifierValueRange.x, modifierValueRange.y) : currentValue + Random.Range(modifierValueRange.x, modifierValueRange.y);
            return returnValue > 0 ? returnValue : 0;
        }
    }
}
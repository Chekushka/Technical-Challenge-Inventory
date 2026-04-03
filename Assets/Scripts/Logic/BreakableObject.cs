using Data;
using UnityEngine;

namespace Logic
{
    public class BreakableObject  : MonoBehaviour
    {
        [SerializeField] private DroppedItem _itemPrefab;
        [SerializeField] private LootGenerator _generator;
        [SerializeField] private ParticleSystem _breakEffect;

        private void OnMouseDown()
        {
            Break();
        }

        public void Break()
        {
            ItemInstance loot = _generator.GenerateRandomLoot();
            
            DroppedItem droppedObj = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            droppedObj.Init(loot);
            droppedObj.Pop();
            
            if (_breakEffect != null) Instantiate(_breakEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
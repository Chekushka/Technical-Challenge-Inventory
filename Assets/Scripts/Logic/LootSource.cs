using Data;
using UnityEngine;

namespace Logic
{
    public class LootSource : MonoBehaviour
    {
        [Header("Generation Settings")]
        [SerializeField] private int _defaultQuantity = 1;
        [SerializeField] private int _maxQuantity = 3;
        [SerializeField] private bool isRandomQuantity = false;
        [SerializeField] private DroppedItem _itemPrefab;
        
        private const float SpawnRadius = 1f;
        
        public void DropLoot()
        {
            int count = isRandomQuantity ? Random.Range(_defaultQuantity, _maxQuantity + 1) : _defaultQuantity;

            for (int i = 0; i < count; i++)
            {
                SpawnSingleItem();
            }
        }

        private void SpawnSingleItem()
        {
            ItemInstance data = LootGenerator.Instance.GenerateRandomLoot();
            
            Vector3 spawnPos = transform.position;
            Vector2 randomOffset = Random.insideUnitCircle * SpawnRadius;
            spawnPos += new Vector3(randomOffset.x, 0, randomOffset.y);
            
            DroppedItem droppedItem = Instantiate(_itemPrefab, spawnPos, Quaternion.identity);
            droppedItem.Init(data);
            droppedItem.Pop();
        }
    }
}
using Data;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(LootSource))]
    public class BreakableObject  : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _breakEffect;
        private LootSource _lootSource;
        
        private void Awake() => _lootSource = GetComponent<LootSource>();

        public void Break()
        {
            if (_breakEffect != null) Instantiate(_breakEffect, transform.position, Quaternion.identity);
            if (_lootSource != null) _lootSource.DropLoot();

            Destroy(gameObject);
        }
    }
}
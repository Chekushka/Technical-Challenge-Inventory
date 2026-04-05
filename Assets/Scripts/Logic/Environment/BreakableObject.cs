using UnityEngine;

namespace Logic.Environment
{
    [RequireComponent(typeof(LootSource))]
    public class BreakableObject  : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private ParticleSystem _breakEffect;
        private LootSource _lootSource;
        
        private void Awake() => _lootSource = GetComponent<LootSource>();

        public void Break()
        {
            if (_breakEffect != null)
            {
                var fx = Instantiate(_breakEffect, transform.position, Quaternion.identity);
                var main = fx.main;
                main.startColor = _renderer.material.color;
                fx.Play();
            }

            
            if (_lootSource != null) _lootSource.DropLoot();

            Destroy(gameObject);
        }
    }
}
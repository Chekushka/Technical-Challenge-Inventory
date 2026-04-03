using Data;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroppedItem : MonoBehaviour, IInteractable
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private ItemInstance _item;
        
        [Header("References")]
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private GameObject _interactionUI;
        
        [Header("Settings")]
        [SerializeField] private float _idleEmissionIntensity = 1.5f;
        [SerializeField] private float _hoverEmissionIntensity = 4.0f;
        
        private Material _materialInstance;
        private Color _rarityColor;

        public void Init(ItemInstance item)
        {
            _item = item;
            _rarityColor = item.GetDisplayColor();
            
            _materialInstance = _meshRenderer.material;
            _materialInstance.EnableKeyword("_EMISSION");
            SetEmission(_idleEmissionIntensity);
            _interactionUI.SetActive(false);
        }

        public void Pop()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 randomDirection = Vector3.up + Random.insideUnitSphere * 0.5f;
            rb.AddForce(randomDirection.normalized * 5f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 10f);
        }
        
        private void SetEmission(float intensity)
        {
            _materialInstance.SetColor(EmissionColor, _rarityColor * intensity);
        }

        public void OnHoverEnter()
        {
            SetEmission(_hoverEmissionIntensity);
            _interactionUI.SetActive(true);
        }

        public void OnHoverExit()
        {
            SetEmission(_idleEmissionIntensity);
            _interactionUI.SetActive(false);
        }

        public void Interact()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.AddItem(_item);
                Destroy(gameObject);
            }
        }
    }
}
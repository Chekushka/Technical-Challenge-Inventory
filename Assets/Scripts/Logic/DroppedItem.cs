using System.Collections;
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
        
        [Header("Settings")]
        [SerializeField] private float _idleEmissionIntensity = 1.5f;
        [SerializeField] private float _hoverEmissionIntensity = 4.0f;
        
        private Material _materialInstance;
        private Color _rarityColor;

        public void Init(ItemInstance item)
        {
            _item = item;
            _rarityColor = item.GetColor();
            
            _materialInstance = _meshRenderer.material;
            _materialInstance.EnableKeyword("_EMISSION");
            SetEmission(_idleEmissionIntensity);
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
        }

        public void OnHoverExit()
        {
            SetEmission(_idleEmissionIntensity);
        }

        public void Interact()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.AddItem(_item);
                StartCoroutine(PickUpSequence());
            }
        }
        
        private IEnumerator PickUpSequence()
        {
            GetComponent<Collider>().enabled = false;

            Vector3 startScale = transform.localScale;
            float elapsed = 0f;
            float duration = 0.2f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsed / duration);
                yield return null;
            }

            InventoryController.Instance.AddItem(_item);
            Destroy(gameObject);
        }
    }
}
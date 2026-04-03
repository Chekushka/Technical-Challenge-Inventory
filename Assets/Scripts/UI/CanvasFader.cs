using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CanvasFader : MonoBehaviour
    {
        [SerializeField] private Image faderImage;
        [SerializeField] private CanvasGroup canvasGroup;

        private Material _instancedMaterial;
        private static readonly int ProgressId = Shader.PropertyToID("_Progress");
        
        public static CanvasFader Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            if (faderImage != null)
            {
                _instancedMaterial = Instantiate(faderImage.material);
                faderImage.material = _instancedMaterial;
                
                _instancedMaterial.SetFloat(ProgressId, 0f);
            }
        
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 1;
        }

        public async Task FadeIn(float duration)
        {
            canvasGroup.blocksRaycasts = true;
            
            await _instancedMaterial.DOFloat(1f, ProgressId, duration)
                .SetUpdate(true)
                .SetEase(Ease.InOutQuad) 
                .AsyncWaitForCompletion();
        }

        public async Task FadeOut(float duration)
        {
            await _instancedMaterial.DOFloat(0f, ProgressId, duration)
                .SetUpdate(true)
                .SetEase(Ease.InOutQuad)
                .AsyncWaitForCompletion();

            canvasGroup.blocksRaycasts = false;
        }
    }
}
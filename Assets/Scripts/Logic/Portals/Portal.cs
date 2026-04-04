using System;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Portals
{
    public class Portal : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TeleportPointId destinationId;
        public UnityEvent onTeleport;

        private TeleportService _teleportService;
        private CanvasFader _fader;

        private void Start()
        {
            _teleportService = TeleportServiceProvider.Instance.TeleportService;
            _fader = CanvasFader.Instance;
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                await ExecuteTeleport(other.gameObject);
            }
        }

        private async Task ExecuteTeleport(GameObject player)
        {
            Transform destination = _teleportService.GetPoint(destinationId);
            if (destination == null) return;

            await _fader.FadeIn(0.3f);

            var inputProvider = player.GetComponent<PlayerInputProvider>();
            inputProvider.IsLocked = true;

            player.transform.position = destination.position;
            player.transform.rotation = destination.rotation;
            Physics.SyncTransforms();
            onTeleport?.Invoke();
            
            await Task.Yield();
            inputProvider.IsLocked = false;
        
            await _fader.FadeOut(0.3f);
        }
    }
}
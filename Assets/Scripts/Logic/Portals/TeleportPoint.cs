using UnityEngine;

namespace Logic.Portals
{
    public class TeleportPoint : MonoBehaviour
    {
        [SerializeField] private TeleportPointId pointId;

        private TeleportService _teleportService;

        private void Awake()
        {
            _teleportService = TeleportServiceProvider.Instance.TeleportService;
            _teleportService.RegisterPoint(pointId, transform);
        }

        private void OnDestroy()
        {
            _teleportService.UnregisterPoint(pointId);
        }
    }
    
    public enum TeleportPointId
    {
        None,
        Hub,
        LootLevel
    }
}
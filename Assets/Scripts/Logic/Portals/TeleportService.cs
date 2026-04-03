using System.Collections.Generic;
using UnityEngine;

namespace Logic.Portals
{
    public class TeleportService
    {
        private readonly Dictionary<TeleportPointId, Transform> _points = new();

        public void RegisterPoint(TeleportPointId id, Transform pointTransform)
        {
            if (!_points.ContainsKey(id))
                _points.Add(id, pointTransform);
        }

        public void UnregisterPoint(TeleportPointId id)
        {
            if (_points.ContainsKey(id))
                _points.Remove(id);
        }

        public Transform GetPoint(TeleportPointId id)
        {
            if (_points.TryGetValue(id, out var point))
                return point;

            Debug.LogError($"[TeleportService] Point with ID {id} not found!");
            return null;
        }
    }
}
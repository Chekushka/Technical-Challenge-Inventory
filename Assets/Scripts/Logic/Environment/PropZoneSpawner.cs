using System.Collections.Generic;
using UnityEngine;

namespace Logic.Environment
{
    public class PropZoneSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private List<GameObject> _propPrefabs;
        [SerializeField] private int _propCount = 10;
        [SerializeField] private Vector3 _zoneSize = new Vector3(10, 0, 10);

        [Header("Placement Rules")]
        [SerializeField] private float _minDistanceBetweenProps = 1.5f;
        [SerializeField] private LayerMask _obstacleLayer;

        private List<Vector3> _spawnedPositions = new List<Vector3>();
    
        public void ResetZone()
        {
            ClearProps();
            SpawnProps();
        }
    
        public void ClearProps()
        {
            _spawnedPositions.Clear();
        
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void SpawnProps()
        {
            int attempts = 0;
            int spawnedCount = 0;

            while (spawnedCount < _propCount && attempts < _propCount * 5)
            {
                Vector3 randomPos = GetRandomPositionInZone();

                if (IsPositionValid(randomPos))
                {
                    GameObject prefab = _propPrefabs[Random.Range(0, _propPrefabs.Count)];
                    Instantiate(prefab, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                
                    _spawnedPositions.Add(randomPos);
                    spawnedCount++;
                }
                attempts++;
            }
        }
    
        private Vector3 GetRandomPositionInZone() => transform.position + new Vector3(Random.Range(-_zoneSize.x / 2, _zoneSize.x / 2), 0, Random.Range(-_zoneSize.z / 2, _zoneSize.z / 2));

        private bool IsPositionValid(Vector3 position)
        {
            foreach (var pos in _spawnedPositions)
            {
                if (Vector3.Distance(position, pos) < _minDistanceBetweenProps) return false;
            }
            return !Physics.CheckSphere(position, _minDistanceBetweenProps / 2, _obstacleLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawCube(transform.position, _zoneSize);
        }
    }
}
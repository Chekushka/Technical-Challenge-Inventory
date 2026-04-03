using UnityEngine;

namespace Logic
{
    public class ItemIdleVisuals : MonoBehaviour
    {
        [SerializeField] private Transform _visualMesh;
        [SerializeField] private float _rotationSpeed = 50f;
        [SerializeField] private float _floatAmplitude = 0.2f;
        [SerializeField] private float _floatFrequency = 1.5f;

        private Vector3 _startPos;

        private void Start()
        {
            _startPos = _visualMesh.localPosition;
        }

        private void Update()
        {
            _visualMesh.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
            
            float newY = _startPos.y + Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;
            _visualMesh.localPosition = new Vector3(_startPos.x, newY, _startPos.z);
        }
    }
}
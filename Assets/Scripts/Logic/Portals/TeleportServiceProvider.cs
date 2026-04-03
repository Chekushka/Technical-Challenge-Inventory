using System;
using UnityEngine;

namespace Logic.Portals
{
    public class TeleportServiceProvider : MonoBehaviour
    {
        public TeleportService TeleportService { get; private set; }
        public static TeleportServiceProvider Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnValidate()
        {
            TeleportService = new TeleportService();
        }
    }
}
using Logic.Core;
using UnityEngine;

namespace Logic.Environment
{
    public class ScrapTable :  MonoBehaviour, IInteractable
    {
        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void Interact()
        {
            InventoryView.Instance.OpenInScrapMode();
        }
    }
}
namespace Logic.Core
{
    public interface IInteractable
    {
        void OnHoverEnter();
        void OnHoverExit();
        void Interact();
    }
}
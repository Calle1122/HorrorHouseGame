using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        Transform GetTransform();
        void Interact(IInteraction interaction);
        void StopInteract();
        void ToggleUI();
        void ToggleOnUI();
        void ToggleOffUI();
    }
}
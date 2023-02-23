using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        Transform GetTransform();
        void StartInteract(IInteraction interaction);
        void StopInteract();
        void ToggleUI();
        void ToggleOnUI();
        void ToggleOffUI();
    }
}
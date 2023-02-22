using Interaction;
using UnityEngine;

public interface IInteraction
{
    Transform GetTransform();
    void AddPossibleInteractable(IInteractable interactable);
    void RemovePossibleInteractable(IInteractable interactable);
    void OnInteract();
    void StopInteract();
}
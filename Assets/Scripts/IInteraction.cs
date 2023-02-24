using Interaction;
using UnityEngine;

public interface IInteraction
{
    Transform GetTransform();
    Transform GetHandTransform();
    void AddPossibleInteractable(IInteractable interactable);
    void RemovePossibleInteractable(IInteractable interactable);
    void OnInteract();
    void StopInteract();
}
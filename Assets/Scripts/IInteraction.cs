using Interaction;
using UnityEngine;

public interface IInteraction
{
    Transform GetTransform();
    void AddPossibleInteractable(Interactable interactable);
    void RemovePossibleInteractable(Interactable interactable);
    void OnInteract();
    void StopInteract();
}
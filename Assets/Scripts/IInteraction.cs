using Interaction;
using UnityEngine;

public interface IInteraction
{
    Transform GetTransform();
    void AddPossibleInteractable(IInteractable pickUpInteractable);
    void RemovePossibleInteractable(IInteractable pickUpInteractable);
    void OnInteract();
    void StopInteract();
}
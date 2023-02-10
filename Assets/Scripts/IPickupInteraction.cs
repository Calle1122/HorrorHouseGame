public interface IPickupInteraction
{
    void AddPossibleInteractable(Interactable interactable);
    void RemovePossibleInteractable(Interactable interactable);
    void Interact();
    void StopInteract();
}
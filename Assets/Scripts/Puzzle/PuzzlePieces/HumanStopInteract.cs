using Interaction;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class HumanStopInteract : MonoBehaviour
    {
        public void StopInteract(PickUpInteractable interactable)
        {
            var humanInteraction = interactable.ownerTransform.GetComponent<PickupInteraction>();
            humanInteraction.StopInteract();
            humanInteraction.RemovePossibleInteractable(interactable);
        }
    }
}

using Interaction;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class HumanStopInteract : MonoBehaviour
    {
        public void StopInteract(PickUpInteractable interactable)
        {
            interactable.ownerTransform.GetComponent<PickupInteraction>().StopInteract();
        }
    }
}

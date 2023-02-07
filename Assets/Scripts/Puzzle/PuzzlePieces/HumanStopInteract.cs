using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class HumanStopInteract : MonoBehaviour
    {
        public void StopInteract(Interactable interactable)
        {
            interactable.ownerTransform.GetComponent<HumanInteraction>().StopInteract();
        }
    }
}

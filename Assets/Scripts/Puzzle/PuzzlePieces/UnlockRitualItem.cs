using Audio;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class UnlockRitualItem : MonoBehaviour
    {
        public void UnlockItem(int itemIndex)
        {
            //TODO: Reference future itemManager (or similar) to say that the item has been unlocked.
            
            DialogueCanvas.Instance.UnlockRitualItem(itemIndex);
        }
    }
}

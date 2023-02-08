using Audio;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class DialogueLine : MonoBehaviour
    {
        public void QueueDialogue(DialogueSo dialogue)
        {
            DialogueCanvas.Instance.QueueDialogue(dialogue);
        }
    }
}

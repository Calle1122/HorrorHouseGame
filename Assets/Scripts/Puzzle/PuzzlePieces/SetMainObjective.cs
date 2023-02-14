using Audio;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class SetMainObjective : MonoBehaviour
    {
        public void SetObjective(string newObjective)
        {
            DialogueCanvas.Instance.UpdateMainObjective(newObjective);
        }
    }
}

using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class DestroyObject : MonoBehaviour
    {
        public void DestroyObj(GameObject reference)
        {
            Destroy(reference);
        }
    }
}

using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class EnableDisable : MonoBehaviour
    {
        public void OnOffObj(GameObject reference)
        {
            reference.SetActive(!reference.activeSelf);
        }
    }
}
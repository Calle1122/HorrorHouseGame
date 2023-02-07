using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class EnableDisable : MonoBehaviour
    {
        public void OnOffObj(GameObject reference)
        {
            switch (reference.activeSelf)
            {
                case true:
                    reference.SetActive(false);
                    break;

                case false:
                    reference.SetActive(true);
                    break;
            }
        }
    }
}
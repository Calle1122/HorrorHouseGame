using System.Collections;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class EnableDisable : MonoBehaviour
    {
        public void OnOffObj(GameObject reference)
        {
            reference.SetActive(!reference.activeSelf);
        }

        public void ToggleDelay(GameObject target)
        {
            StartCoroutine(DelayToggle(target));
        }

        private IEnumerator DelayToggle(GameObject target)
        {
            yield return new WaitForSeconds(1f);
            target.SetActive(true);
        }
    }
}
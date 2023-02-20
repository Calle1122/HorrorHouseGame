using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class PlayAnimationOnce : MonoBehaviour
    {
        public void PlayAnimation(GameObject target)
        {
            target.GetComponent<Animator>().SetBool("playOnce", true);
        }
    }
}

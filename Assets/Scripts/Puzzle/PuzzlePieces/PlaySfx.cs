using Audio;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class PlaySfx : MonoBehaviour
    {
        public void PlaySound(SfxSO sound)
        {
            SoundManager.Instance.PlaySfx(sound);
        }
    }
}

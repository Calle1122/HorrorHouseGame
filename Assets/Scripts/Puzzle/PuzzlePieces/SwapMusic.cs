using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class SwapMusic : MonoBehaviour
    {
        public void NewMusic(AudioClip newMusic)
        {
            var source = Camera.main.GetComponent<AudioSource>();
            source.Stop();
            source.clip = newMusic;
            source.Play();
        } 
    }
}

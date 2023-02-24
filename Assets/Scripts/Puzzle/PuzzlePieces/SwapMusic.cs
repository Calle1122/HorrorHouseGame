using System.Collections;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class SwapMusic : MonoBehaviour
    {
        public void NewMusic(AudioClip newMusic)
        {
            AudioSource source = Camera.main.GetComponent<AudioSource>();
            float orgVolume = source.volume;
            
            StartCoroutine(VolumeLerp(0f, orgVolume, source, newMusic, true));
        }

        private IEnumerator VolumeLerp(float targetVolume, float orgVol, AudioSource source, AudioClip newMusic, bool startAgain)
        {
            var currentTime = 0f;
            var currentVol = source.volume;
            
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                var newVolume = Mathf.Lerp(currentVol, targetVolume, currentTime / 1f);
                source.volume = newVolume;
                yield return null;
            }

            if (startAgain)
            {
                source.clip = newMusic;
                source.Play();
                StartCoroutine(VolumeLerp(orgVol, orgVol, source, newMusic, false));
            }
        }
    }
}

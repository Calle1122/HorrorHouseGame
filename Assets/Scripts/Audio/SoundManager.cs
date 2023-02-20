using UnityEngine;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        
        public GameObject musicSource, sfxSource, dialogueSource, ambientSource;
        
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void PlaySfx(SfxSO soundEffect)
        {
            if (soundEffect == null)
            {
                return;
            }
            soundEffect.Play();
        }
    }
}
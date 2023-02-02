using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {/*

        //public static AudioMixerGroup MusicMixer, SfxMixer;
        
        public static void PlaySoundFX(AudioClip soundToPlay, SoundType soundType)
        {
            GameObject soundObj = new GameObject("Sound");
            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = GetMixer(soundType);
            audioSource.PlayOneShot(soundToPlay);
        }

        public static AudioMixerGroup GetMixer(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Music:
                    return MusicMixer;


                case SoundType.SoundEffect:
                    return SfxMixer;
            }

            return default;
        }*/
    }

    public enum SoundType
    {
        Music,
        SoundEffect
    }
}

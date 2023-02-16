using GameConstants;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private Slider masterSlider, musicSlider, sfxSlider, dialogueSlider, ambientSlider;

        private void Awake()
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            masterSlider.value = 0.5f;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            musicSlider.value = 0.5f;
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            sfxSlider.value = 0.5f;
            dialogueSlider.onValueChanged.AddListener(SetDialogueVolume);
            dialogueSlider.value = 0.5f;
            ambientSlider.onValueChanged.AddListener(SetAmbientVolume);
            ambientSlider.value = 0.5f;
        }

        public void SetMasterVolume(float value)
        {
            masterMixer.SetFloat(Strings.MasterVol, Mathf.Log10(value) * 20);
        }

        private void SetMusicVolume(float value)
        {
            masterMixer.SetFloat(Strings.MusicVol, Mathf.Log10(value) * 20);
        }

        private void SetSfxVolume(float value)
        {
            masterMixer.SetFloat(Strings.SfxVol, Mathf.Log10(value) * 20);
        }

        private void SetDialogueVolume(float value)
        {
            masterMixer.SetFloat(Strings.DialogueVol, Mathf.Log10(value) * 20);
        }

        private void SetAmbientVolume(float value)
        {
            masterMixer.SetFloat(Strings.AmbientVol, Mathf.Log10(value) * 20);
        }
    }
}
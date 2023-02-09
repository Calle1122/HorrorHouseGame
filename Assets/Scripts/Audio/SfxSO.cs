using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(fileName = "NewSfx", menuName = "Sound/New Sfx")]
    public class SfxSO : ScriptableObject
    {
        public AudioClip[] clips;
        public Vector2 volume = new Vector2(.5f, .5f);
        public Vector2 pitch = new Vector2(1, 1);
        [SerializeField] private int playIndex;
        [SerializeField] private ClipPlayOrder playOrder;

        public SourceType sourceType;
        
        public enum SourceType
        {
            Music,
            Sfx,
            Dialogue,
            Ambient
        }

        #region PreviewSoundCode
        
        #if UNITY_EDITOR

        private AudioSource _previewer;

        private void OnEnable()
        {
            _previewer = EditorUtility
                .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave, typeof(AudioSource))
                .GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(_previewer.gameObject);
        }

        [ContextMenu("Play Preview")]
        public void PlayPreview()
        {
            if (_previewer != null)
            {
                Play(_previewer);
            }
            else
            {
                Debug.Log("Previewer is null");
            }
        }

        public void StopPreview()
        {
            _previewer.Stop();
        }

        #endif
        
        #endregion

        private AudioClip GetAudioClip()
        {

            var clip = clips[playIndex >= clips.Length ? 0 : playIndex];
            
            switch (playOrder)
            {
                case ClipPlayOrder.Random:
                    playIndex = Random.Range(0, clips.Length);
                    break;
                
                case ClipPlayOrder.InOrder:
                    playIndex = (playIndex + 1) % clips.Length;
                    break;
                
                case ClipPlayOrder.Reverse:
                    playIndex = (playIndex + clips.Length - 1) % clips.Length;
                    break;
            }
            
            return clip;
        }
        
        public AudioSource Play(AudioSource audioSource = null)
        {
            if (clips != null)
            {
                if (clips.Length == 0)
                {
                    Debug.Log("No clips added!");
                    return null;
                }

                var source = audioSource;
                if (source == null)
                {
                    var sourceObj = GetSourceObj(sourceType);
                    GameObject newSource = Instantiate(sourceObj, Vector3.zero, Quaternion.identity);
                    source = newSource.GetComponent<AudioSource>();
                }

                source.clip = GetAudioClip();
                source.volume = Random.Range(volume.x, volume.y);
                source.pitch = Random.Range(pitch.x, pitch.y);
            
                source.Play();
            
                #if UNITY_EDITOR

                if (source != _previewer)
                {
                    Destroy(source.gameObject, source.clip.length/source.pitch);
                }
            
                #else
            
            Destroy(source.gameObject, source.clip.length/source.pitch);
            
                #endif

                return source;
            }

            else
            {
                Debug.Log("Clip array is null");
                return null;
            }
            
        }

        private GameObject GetSourceObj(SourceType type)
        {
            switch (type)
            {
                case SourceType.Music:
                    return SoundManager.Instance.musicSource;
                case SourceType.Sfx:
                    return SoundManager.Instance.sfxSource;
                case SourceType.Dialogue:
                    return SoundManager.Instance.dialogueSource;
                case SourceType.Ambient:
                    return SoundManager.Instance.ambientSource;
                default:
                    Debug.LogError("Invalid audio source type", this);
                    break;
            }

            return null;
        }

        enum ClipPlayOrder
        {
            Random,
            InOrder,
            Reverse
        }
        
    }
}

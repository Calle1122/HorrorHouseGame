using System;
using Audio;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public static CutsceneController Instance { get; private set; }

    [SerializeField] private VideoPlayer introCutscenePlayer, outroCutscenePlayer;
    [SerializeField] private GameObject gameOverScreen;

    private AudioSource _musicSource;
    
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
        
        _musicSource = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        introCutscenePlayer.Prepare();
    }

    public float GetIntroLength()
    {
        return (float)introCutscenePlayer.clip.length;
    }
    
    public void PlayIntroCutscene()
    {
        _musicSource.mute = true;
        DialogueCanvas.Instance.gameObject.SetActive(false);
        
        introCutscenePlayer.Play();
        Invoke(nameof(StopIntroCutscene), (float)introCutscenePlayer.clip.length);
    }

    private void StopIntroCutscene()
    {
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        introCutscenePlayer.gameObject.SetActive(false);
    }
    
    public float GetOutroLength()
    {
        return (float)outroCutscenePlayer.clip.length;
    }
    
    public void PlayOutroCutscene()
    {
        _musicSource.mute = true;
        DialogueCanvas.Instance.gameObject.SetActive(false);
        
        outroCutscenePlayer.Play();
        Invoke(nameof(StopOutroCutscene), (float)outroCutscenePlayer.clip.length);
    }

    private void StopOutroCutscene()
    {
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        outroCutscenePlayer.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}

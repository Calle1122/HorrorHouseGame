using System;
using System.Collections;
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

        Game.Input.GhostInputMode = InputMode.Limited;
        Game.Input.HumanInputMode = InputMode.Limited;
        
        introCutscenePlayer.Play();
        Invoke(nameof(StopIntroCutscene), (float)introCutscenePlayer.clip.length);
    }

    private void StopIntroCutscene()
    {
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        introCutscenePlayer.gameObject.SetActive(false);
        
        Game.Input.GhostInputMode = InputMode.Free;
        Game.Input.HumanInputMode = InputMode.Free;
    }

    public void ForceStopIntro()
    {
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        introCutscenePlayer.gameObject.SetActive(false);
        
        Game.Input.GhostInputMode = InputMode.Free;
        Game.Input.HumanInputMode = InputMode.Free;
    }

    public void PlayOutroCutscene()
    {
        StartCoroutine(DelaySkipCutsceneSubscription());
        
        _musicSource.mute = true;
        DialogueCanvas.Instance.gameObject.SetActive(false);
        
        outroCutscenePlayer.Play();
        Invoke(nameof(StopOutroCutscene), (float)outroCutscenePlayer.clip.length);
    }
    
    private IEnumerator DelaySkipCutsceneSubscription()
    {
        yield return new WaitForSeconds(3.5f);
        Game.Input.OnHumanInteract.AddListener(ForceStopOutro);
    }

    private void StopOutroCutscene()
    {
        Game.Input.OnHumanInteract.RemoveListener(ForceStopOutro);
        
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        outroCutscenePlayer.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }
    
    public void ForceStopOutro()
    {
        Game.Input.OnHumanInteract.RemoveListener(ForceStopOutro);
        
        _musicSource.mute = false;
        DialogueCanvas.Instance.gameObject.SetActive(true);
        outroCutscenePlayer.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}

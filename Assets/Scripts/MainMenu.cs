using System.Collections;
using Events;
using Puzzle;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PhantomTetherEvent gameStartEvent;
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private GameObject titleTextObject;
    [SerializeField] private GameObject buttonsObject;
    [SerializeField] private GameObject popupObject;
    [SerializeField] private GameObject fetchingDevicesObject;
    [SerializeField] private GameObject aboutObject;
    [SerializeField] private GameObject settingsObject;

    private Coroutine _cutsceneCoroutine;

    private void Start()
    {
        ClearUI(false);
        ShowMenu();
    }

    // BUTTON CALLBACK
    public void StartGame(bool allowKeyboard)
    {
        ClearUI(false);
        fetchingDevicesObject.SetActive(true);
        StartCoroutine(TryStartGame(allowKeyboard));
    }

    // BUTTON CALLBACK
    public void Settings()
    {
        ClearUI(true);
        settingsObject.SetActive(true);
    }

    public void ShowAbout()
    {
        ClearUI(false);
        aboutObject.SetActive(true);
    }

    // BUTTON CALLBACK
    public void QuitGame()
    {
        Application.Quit();
    }

    // BUTTON CALLBACK
    public void ShowMenu()
    {
        ClearUI(false);
        titleTextObject.SetActive(true);
        buttonsObject.SetActive(true);
    }

    private IEnumerator TryStartGame(bool allowKeyboard)
    {
        yield return StartCoroutine(Game.Input.InitializeGame(allowKeyboard));
        menuRoot.SetActive(false);
        
        //CUTSCENE
        StartCoroutine(DelaySkipCutsceneSubscription());
        
        CutsceneController.Instance.PlayIntroCutscene();
        _cutsceneCoroutine = StartCoroutine(DelayGameStart(CutsceneController.Instance.GetIntroLength()));

    }

    private IEnumerator DelaySkipCutsceneSubscription()
    {
        yield return new WaitForSeconds(3.5f);
        Game.Input.OnHumanInteract.AddListener(ForceStopIntro);
    }
    
    private IEnumerator DelayGameStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Game.Input.OnHumanInteract.RemoveListener(ForceStopIntro);
        
        gameStartEvent.RaiseEvent();
        ShowMenu();
    }

    private void ForceStopIntro()
    {
        Game.Input.OnHumanInteract.RemoveListener(ForceStopIntro);
        StopCoroutine(_cutsceneCoroutine);
        
        CutsceneController.Instance.ForceStopIntro();
        gameStartEvent.RaiseEvent();
        ShowMenu();
    }

    public void PopUp()
    {
        StopAllCoroutines();
        ClearUI(false);
        ShowPopup();
    }

    private void ClearUI(bool turnOffTitleText)
    {
        if (turnOffTitleText)
        {
            titleTextObject.SetActive(false);
        }

        popupObject.SetActive(false);
        fetchingDevicesObject.SetActive(false);
        buttonsObject.SetActive(false);
        aboutObject.SetActive(false);
        settingsObject.SetActive(false);
    }

    private void ShowPopup()
    {
        popupObject.SetActive(true);
    }
}
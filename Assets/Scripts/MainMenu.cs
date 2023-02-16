using System.Collections;
using Events;
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

    private void Start()
    {
        ClearUI(false);
        ShowMenu();
    }

    // BUTTON CALLBACK
    public void StartGame()
    {
        ClearUI(false);
        fetchingDevicesObject.SetActive(true);
        StartCoroutine(TryStartGame());
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

    private IEnumerator TryStartGame()
    {
        yield return StartCoroutine(Game.Input.InitializeGame());
        menuRoot.SetActive(false);
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
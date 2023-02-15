using System.Collections;
using Events;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private DefaultEvent gameStartEvent;
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private GameObject buttonsObject;
    [SerializeField] private GameObject popupObject;
    [SerializeField] private GameObject fetchingDevicesObject;

    private void Start()
    {
        popupObject.SetActive(false);
        fetchingDevicesObject.SetActive(false);
    }

    // BUTTON CALLBACK
    public void StartGame()
    {
        ClearUI();
        fetchingDevicesObject.SetActive(true);
        StartCoroutine(TryStartGame());
    }

    private IEnumerator TryStartGame()
    {
        yield return StartCoroutine(Game.Input.InitializeGame());
        menuRoot.SetActive(false);
    }

    public void PopUp()
    {
        StopAllCoroutines();
        ClearUI();
        ShowPopup();
    }

    private void ClearUI()
    {
        popupObject.SetActive(false);
        fetchingDevicesObject.SetActive(false);
        buttonsObject.SetActive(false);
    }

    private void ShowPopup()
    {
        popupObject.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConstants;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject humanPrefab;
    [SerializeField] private GameObject ghostPrefab;

    public UnityEvent<Vector3> OnGhostMovementInput;
    public UnityEvent<Vector3> OnGhostNoMovementInput;

    public UnityEvent<Vector3> OnHumanMovementInput;
    public UnityEvent<Vector3> OnHumanNoMovementInput;

    public UnityEvent OnGhostJumpPressed;
    public UnityEvent OnGhostJumpReleased;

    public UnityEvent OnHumanJumpPressed;
    public UnityEvent OnHumanJumpReleased;

    public UnityEvent OnHumanInteract;
    public UnityEvent OnGhostInteract;

    public UnityEvent OnHumanCancel;
    public UnityEvent OnGhostCancel;

    private List<InputDevice> controllers = new List<InputDevice>();
    private GameObject ghostPlayer;
    private GameObject humanPlayer;
    private InputAction inputActions;
    private PlayerInput player1Input;
    private PlayerInput player2Input;

    public InputMode GhostInputMode { get; set; }
    public InputMode HumanInputMode { get; set; }

    public GameObject GhostPlayer => ghostPlayer;
    public GameObject HumanPlayer => humanPlayer;

    private void Start()
    {
        GhostInputMode = InputMode.Free;
        HumanInputMode = InputMode.Free;
    }

    private void OnDisable()
    {
        UnsubscribeInputActions();
    }

    private void UnsubscribeInputActions()
    {
        if (player1Input == null)
        {
            Debug.LogWarning($"[{this}] Player1 Input was null, unable to unsubscribe InputActions");
            return;
        }

        foreach (var action in player1Input.currentActionMap.actions)
        {
            Debug.Log($"Unsubscribed {action}.");
            switch (action.name)
            {
                case Strings.Move:
                    action.performed -= HumanMovementInput;
                    action.canceled -= HumanNoMovementInput;
                    break;
                case Strings.Jump:
                    action.started -= HumanJumpPressed;
                    action.canceled -= HumanJumpReleased;
                    break;
                case Strings.Interact:
                    action.started -= HumanInteract;
                    break;
                case Strings.Cancel:
                    action.started -= HumanCancel;
                    break;
            }
        }

        if (player2Input == null)
        {
            Debug.LogWarning($"[{this}] Player2 Input was null, unable to unsubscribe InputActions");
            return;
        }

        {
            foreach (var action in player2Input.currentActionMap.actions)
            {
                Debug.Log($"Unsubscribed {action}.");
                switch (action.name)
                {
                    case Strings.Move:
                        action.performed -= GhostMovementInput;
                        action.canceled -= GhostNoMovementInput;
                        break;
                    case Strings.Jump:
                        action.started -= GhostJumpPressed;
                        action.canceled -= GhostJumpReleased;
                        break;
                    case Strings.Interact:
                        action.started -= GhostInteract;
                        break;
                    case Strings.Cancel:
                        action.started -= GhostCancel;
                        break;
                }
            }
        }
    }

    public IEnumerator InitializeGame(bool allowKeyboard)
    {
        controllers = FetchAllGamepads();

        yield return new WaitForSeconds(2f);

        if (allowKeyboard)
        {
            if (controllers.Count < 1)
            {
                UIManager.ShowPopup();
                yield break;
            }
        }
        else if (controllers.Count < 2)
        {
            UIManager.ShowPopup();
            yield break;
        }

        SpawnPlayerInput(allowKeyboard);
        SpawnCharacters();
    }

    // TODO: Create a event handler to add listeners and subscribe to events, separate the logic

    private void HumanMovementInput(InputAction.CallbackContext context)
    {
        var newMovementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        OnHumanMovementInput.Invoke(newMovementInput);
    }

    private void HumanNoMovementInput(InputAction.CallbackContext obj)
    {
        OnHumanNoMovementInput.Invoke(Vector3.zero);
    }

    private void GhostMovementInput(InputAction.CallbackContext context)
    {
        var newMovementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        OnGhostMovementInput.Invoke(newMovementInput);
    }

    private void GhostNoMovementInput(InputAction.CallbackContext obj)
    {
        OnGhostNoMovementInput.Invoke(Vector3.zero);
    }

    private void GhostJumpPressed(InputAction.CallbackContext context)
    {
        OnGhostJumpPressed.Invoke();
    }

    private void GhostJumpReleased(InputAction.CallbackContext obj)
    {
        OnGhostJumpReleased.Invoke();
    }

    private void HumanJumpPressed(InputAction.CallbackContext obj)
    {
        OnHumanJumpPressed.Invoke();
    }

    private void HumanJumpReleased(InputAction.CallbackContext obj)
    {
        OnHumanJumpReleased.Invoke();
    }

    private void HumanInteract(InputAction.CallbackContext obj)
    {
        OnHumanInteract.Invoke();
    }

    private void GhostInteract(InputAction.CallbackContext obj)
    {
        OnGhostInteract.Invoke();
    }

    private void SpawnCharacters()
    {
        humanPlayer = Instantiate(humanPrefab, new Vector3(0, 1, 15), quaternion.identity);
        ghostPlayer = Instantiate(ghostPrefab, new Vector3(0, 1, 15), quaternion.identity);

        if (player1Input == null)
        {
            return;
        }

        foreach (var action in player1Input.currentActionMap.actions)
        {
            switch (action.name)
            {
                case Strings.Move:
                    action.performed += HumanMovementInput;
                    action.canceled += HumanNoMovementInput;
                    break;
                case Strings.Jump:
                    action.started += HumanJumpPressed;
                    action.canceled += HumanJumpReleased;
                    break;
                case Strings.Interact:
                    action.started += HumanInteract;
                    break;
                case Strings.Cancel:
                    action.started += HumanCancel;
                    break;
            }
        }

        if (player2Input == null)
        {
            return;
        }

        foreach (var action in player2Input.currentActionMap.actions)
        {
            switch (action.name)
            {
                case Strings.Move:
                    action.performed += GhostMovementInput;
                    action.canceled += GhostNoMovementInput;
                    break;
                case Strings.Jump:
                    action.started += GhostJumpPressed;
                    action.canceled += GhostJumpReleased;
                    break;
                case Strings.Interact:
                    action.started += GhostInteract;
                    break;
                case Strings.Cancel:
                    action.started += GhostCancel;
                    break;
            }
        }
    }

    private void GhostCancel(InputAction.CallbackContext obj)
    {
        OnGhostCancel.Invoke();
    }

    private void HumanCancel(InputAction.CallbackContext obj)
    {
        OnHumanCancel.Invoke();
    }


    private void SpawnPlayerInput(bool allowKeyboard)
    {
        if (allowKeyboard)
        {
            // Play with 1 keyboard
            if (player1Input == null)
            {
                player1Input = Game.PlayerInputManager.JoinPlayer(0, -1, null, Keyboard.current);
            }

            if (player2Input == null)
            {
                player2Input = Game.PlayerInputManager.JoinPlayer(1, -1, null, controllers.First());
            }
        }

        else
        {
            // Play with 2 controllers
            foreach (var inputDevice in controllers)
            {
                if (player1Input == null)
                {
                    player1Input = Game.PlayerInputManager.JoinPlayer(0, -1, null, inputDevice);
                }
                else if (player2Input == null)
                {
                    player2Input = Game.PlayerInputManager.JoinPlayer(1, -1, null, inputDevice);
                }
            }
        }
    }

    private static List<InputDevice> FetchAllGamepads()
    {
        var devices = new List<InputDevice>();
        devices.AddRange(Gamepad.all);
        Debug.Log($"Fetched {devices.Count} device(s)");
        foreach (var inputDevice in devices)
        {
            Debug.Log($"{inputDevice}");
        }

        return devices;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (player1Input == null)
        {
            player1Input = playerInput;
        }

        else if (player2Input == null)
        {
            player2Input = playerInput;
        }
    }
}
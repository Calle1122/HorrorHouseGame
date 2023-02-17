using System.Collections;
using System.Collections.Generic;
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

    private GameObject ghostPlayer;
    private GameObject humanPlayer;
    private InputAction inputActions;

    private List<InputDevice> inputDevices = new List<InputDevice>();
    private PlayerInput player1Input;
    private PlayerInput player2Input;

    public InputMode GhostInputMode { get; set; }
    public InputMode HumanInputMode { get; set; }

    private void Start()
    {
        GhostInputMode = InputMode.Free;
        HumanInputMode = InputMode.Free;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPlayerInput();
            SpawnCharacters();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(InitializeGame());
        }
    }
#endif

    private void OnDisable()
    {
        if (player1Input != null)
        {
            foreach (var action in player1Input.currentActionMap.actions)
            {
                Debug.Log($"Unsubscribed {action}.");
                if (action.name == Strings.Move)
                {
                    action.performed -= HumanMovementInput;
                    action.canceled -= HumanNoMovementInput;
                }
                else if (action.name == Strings.Jump)
                {
                    action.started -= HumanJumpPressed;
                    action.canceled -= HumanJumpReleased;
                }
                else if (action.name == Strings.Interact)
                {
                    action.started -= HumanInteract;
                }
                else if (action.name == Strings.Cancel)
                {
                    action.started -= HumanCancel;
                }
            }
        }

        if (player2Input == null)
        {
            return;
        }

        {
            foreach (var action in player2Input.currentActionMap.actions)
            {
                Debug.Log($"Unsubscribed {action}.");
                if (action.name == Strings.Move)
                {
                    action.performed -= GhostMovementInput;
                    action.canceled -= GhostNoMovementInput;
                }
                else if (action.name == Strings.Jump)
                {
                    action.started -= GhostJumpPressed;
                    action.canceled -= GhostJumpReleased;
                }
                else if (action.name == Strings.Interact)
                {
                    action.started -= GhostInteract;
                }
                else if (action.name == Strings.Cancel)
                {
                    action.started -= GhostCancel;
                }
            }
        }
    }

    public IEnumerator InitializeGame()
    {
        inputDevices = GetAllInputDevices();
        yield return new WaitForSeconds(2f);
        if (inputDevices.Count < 2)
        {
            UIManager.ShowPopup();
            yield break;
        }

        SpawnPlayerInput();
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


    private void SpawnPlayerInput()
    {
        foreach (var inputDevice in inputDevices)
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

    private static List<InputDevice> GetAllInputDevices()
    {
        var devices = new List<InputDevice> { Keyboard.current };
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
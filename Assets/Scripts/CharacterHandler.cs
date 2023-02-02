﻿using System.Collections.Generic;
using GameConstants;
using Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterHandler : MonoBehaviour
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

    private GameObject ghostPlayer;
    private GameObject humanPlayer;
    private InputAction inputActions;

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private List<InputDevice> inputDevices = new List<InputDevice>();
    private bool movementInputEnabled;
    private PlayerInput player1Input;
    private PlayerInput player2Input;

    private void Start()
    {
        movementInputEnabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPlayerInput();
            SpawnCharacters();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            inputDevices = GetAllInputDevices();
        }
    }

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
            }
        }
    }

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
        var newMovementInput = context.ReadValue<Vector2>();
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
        humanPlayer = Instantiate(humanPrefab);
        ghostPlayer = Instantiate(ghostPrefab);

        humanPlayer.GetComponent<MovementBase>();
        ghostPlayer.GetComponent<MovementBase>();

        foreach (var action in player1Input.currentActionMap.actions)
        {
            if (action.name == Strings.Move)
            {
                action.performed += HumanMovementInput;
                action.canceled += HumanNoMovementInput;
            }
            else if (action.name == Strings.Jump)
            {
                action.started += HumanJumpPressed;
                action.canceled += HumanJumpReleased;
            }
            else if (action.name == Strings.Interact)
            {
                action.started += HumanInteract;
            }
        }

        if (player2Input == null)
        {
            return;
        }

        foreach (var action in player2Input.currentActionMap.actions)
        {
            if (action.name == Strings.Move)
            {
                action.performed += GhostMovementInput;
                action.canceled += GhostNoMovementInput;
            }
            else if (action.name == Strings.Jump)
            {
                action.started += GhostJumpPressed;
                action.canceled += GhostJumpReleased;
            }
            else if (action.name == Strings.Interact)
            {
                action.started += GhostInteract;
            }
        }

        movementInputEnabled = true;
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
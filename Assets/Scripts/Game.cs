using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Game
{
    public static InputManager Input { get; private set; }
    public static PlayerInputManager PlayerInputManager { get; private set; }
    public static UIManager UI { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGame()
    {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>(nameof(Game)));
        Input = gameObject.GetComponentInChildren<InputManager>();
        PlayerInputManager = gameObject.GetComponentInChildren<PlayerInputManager>();
        UI = gameObject.GetComponentInChildren<UIManager>();
    }
}
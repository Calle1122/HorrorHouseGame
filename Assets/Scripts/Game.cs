using UnityEngine;
using UnityEngine.InputSystem;

public static class Game
{
    public static InputManager Input { get; set; }
    public static PlayerInputManager PlayerInputManager { get; set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGame()
    {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>(nameof(Game)));
        Input = gameObject.GetComponentInChildren<InputManager>();
        PlayerInputManager = gameObject.GetComponentInChildren<PlayerInputManager>();
    }
}
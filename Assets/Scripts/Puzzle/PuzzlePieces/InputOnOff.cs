using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class InputOnOff : MonoBehaviour
    {
        public enum PlayerType
        {
            Human,
            Ghost
        }
        
        public void InputEnableDisable(PlayerType player)
        {
            if (player == PlayerType.Ghost)
            {
                Game.Input.GhostInputMode = Game.Input.GhostInputMode == InputMode.Free ? InputMode.Limited : InputMode.Free;
            }
            else
            {
                Game.Input.HumanInputMode = Game.Input.HumanInputMode == InputMode.Free ? InputMode.Limited : InputMode.Free;
            }
        }
    }
}

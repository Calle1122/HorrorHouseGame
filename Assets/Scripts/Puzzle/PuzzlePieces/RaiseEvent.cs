using Events;
using UnityEngine;

namespace Puzzle.PuzzlePieces
{
    public class RaiseEvent : MonoBehaviour
    {
        public void RaisePhantomEvent(PhantomTetherEvent eventToRaise)
        {
            eventToRaise.RaiseEvent();
        }
    }
}

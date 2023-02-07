using Events;
using UnityEngine;

namespace Puzzle
{
    public class ProgressionTracker : MonoBehaviour
    {
        [SerializeField] private DefaultEvent gameStartEvent;

        [SerializeField] private bool hasStarted = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U) && !hasStarted)
            {
                hasStarted = true;
                gameStartEvent.RaiseEvent();
            }
        }
    }
}
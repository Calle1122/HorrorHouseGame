using Events;
using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private PhantomTetherEvent eventToTrigger;

        private bool _hasTriggered;

        private bool _humanHasEntered, _ghostHasEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (_hasTriggered)
            {
                return;
            }

            if (other.CompareTag(Tags.PlayerTag))
            {
                _humanHasEntered = true;
            }
            if (other.CompareTag(Tags.GhostTag))
            {
                _ghostHasEntered = true;
            }

            TryRaiseEvent();
        }

        private void TryRaiseEvent()
        {
            if (!_humanHasEntered || !_ghostHasEntered)
            {
                return;
            }
            
            eventToTrigger.RaiseEvent();
            _hasTriggered = true;
        }
    }
}
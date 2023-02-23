using Audio;
using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class MainObjectiveTrigger : MonoBehaviour
    {
        [SerializeField] private string newObjective;

        private bool _hasTriggered;
        
        private bool _ghostEntered, _humanEntered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                _humanEntered = true;
            }

            if (other.CompareTag(Tags.GhostTag))
            {
                _ghostEntered = true;
            }
            
            TryChangeObjective();
            
        }

        private void TryChangeObjective()
        {
            if (_hasTriggered)
            {
                return;
            }

            if (!_humanEntered || !_ghostEntered)
            {
                return;
            }
            
            DialogueCanvas.Instance.UpdateMainObjective(newObjective);
            _hasTriggered = true;
        }
    }
}

using Audio;
using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class MainObjectiveTrigger : MonoBehaviour
    {
        [SerializeField] private string newObjective;

        private bool _hasTriggered;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            if (_hasTriggered)
            {
                return;
            }
            DialogueCanvas.Instance.UpdateMainObjective(newObjective);
            _hasTriggered = true;
        }
    }
}

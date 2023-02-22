using System.Collections.Generic;
using GameConstants;
using UnityEngine;

namespace Audio
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private List<DialogueSo> dialogue;

        public TriggerType triggerType;
        
        private bool _hasPlayed = false;

        private bool _humanHasEntered, _ghostHasEntered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.GhostTag))
            {
                _ghostHasEntered = true;
            }
            if (other.CompareTag(Tags.PlayerTag))
            {
                _humanHasEntered = true;
            }
            
            TryPlay();
        }

        private void TryPlay()
        {
            if (!_ghostHasEntered || !_humanHasEntered)
            {
                return;
            }
            
            if (!_hasPlayed)
            {
                foreach (DialogueSo dia in dialogue)
                {
                    DialogueCanvas.Instance.QueueDialogue(dia);
                }
            }
            
            if (triggerType == TriggerType.PlayOnce)
            {
                _hasPlayed = true;
            }
        }
    }

    public enum TriggerType
    {
        PlayOnce,
        Replay
    }
    
}
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private List<DialogueSo> dialogue;

        public TriggerType triggerType;
        
        private bool _hasPlayed = false;
        
        private void OnTriggerEnter(Collider other)
        {
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
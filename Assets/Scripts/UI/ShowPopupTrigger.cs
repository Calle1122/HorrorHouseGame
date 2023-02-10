using Events;
using GameConstants;
using Puzzle;
using UnityEngine;

namespace UI
{
    public class ShowPopupTrigger : MonoBehaviour
    {
        [SerializeField] private NoFunctionPopupTrigger targetPopup;
        [SerializeField] private DefaultEvent eventToTrigger;

        private bool _hasTriggeredEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }
            
            targetPopup.HideShowPopup();

            if (!_hasTriggeredEvent)
            {
                _hasTriggeredEvent = true;
                
                eventToTrigger.RaiseEvent();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }
            
            targetPopup.HideShowPopup();
        }
    }
}

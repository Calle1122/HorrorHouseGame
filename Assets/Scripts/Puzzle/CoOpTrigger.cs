using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class CoOpTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject interactSprite;
        [SerializeField] private CoOpTriggerHandler handler;
        private bool _humanInTrigger, _ghostInTrigger;

        private void OnEnable()
        {
            Game.CharacterHandler.OnHumanInteract.AddListener(OnHumanInteract);
            Game.CharacterHandler.OnGhostInteract.AddListener(OnGhostInteract);
        }

        private void OnDisable()
        {
            Game.CharacterHandler.OnHumanInteract.RemoveListener(OnHumanInteract);
            Game.CharacterHandler.OnGhostInteract.RemoveListener(OnGhostInteract);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            if (other.CompareTag(Tags.PlayerTag))
            {
                _humanInTrigger = true;
            }
            else
            {
                _ghostInTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }
            
            if (other.CompareTag(Tags.PlayerTag))
            {
                _humanInTrigger = false;
            }
            else
            {
                _ghostInTrigger = false;
            }
            
            ToggleInteractUI(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag)) return;
            if (handler.humanIsInteracting && handler.ghostIsInteracting)
            {
                ToggleInteractUI(false);
            }
            else
            {
                ToggleInteractUI(true);
            }

            if (_humanInTrigger && !_ghostInTrigger)
            {
                if (handler.humanIsInteracting)
                {
                    ToggleInteractUI(false);
                }
            }

            if (!_humanInTrigger && _ghostInTrigger)
            {
                if (handler.ghostIsInteracting)
                {
                    ToggleInteractUI(false);
                }
            }
        }

        private void ToggleInteractUI(bool activeState)
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(activeState);
            }
        }

        private void OnGhostInteract()
        {
            if(!_ghostInTrigger)
            {
                return;
            }

            Game.CharacterHandler.GhostInputMode = InputMode.MovementLimited;
            handler.ghostIsInteracting = true;
            handler.GhostInteract();
        }

        private void OnHumanInteract()
        {
            if(!_humanInTrigger)
            {
                return;
            }

            Game.CharacterHandler.HumanInputMode = InputMode.MovementLimited;
            handler.humanIsInteracting = true;
            handler.HumanInteract();
        }
    }
}
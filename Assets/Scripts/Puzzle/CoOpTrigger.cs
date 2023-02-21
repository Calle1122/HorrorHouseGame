using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class CoOpTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject interactSprite;
        [SerializeField] private CoOpTriggerHandler handler;
        private bool humanInTrigger, ghostInTrigger;

        private void OnEnable()
        {
            Game.Input.OnHumanInteract.AddListener(OnHumanInteract);
            Game.Input.OnGhostInteract.AddListener(OnGhostInteract);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(OnHumanInteract);
            Game.Input.OnGhostInteract.RemoveListener(OnGhostInteract);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            if (other.CompareTag(Tags.PlayerTag))
            {
                humanInTrigger = true;
            }
            else
            {
                ghostInTrigger = true;
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
                humanInTrigger = false;
            }
            else
            {
                ghostInTrigger = false;
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

            if (humanInTrigger && !ghostInTrigger)
            {
                if (handler.humanIsInteracting)
                {
                    ToggleInteractUI(false);
                }
            }

            if (!humanInTrigger && ghostInTrigger)
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
            if(!ghostInTrigger)
            {
                return;
            }

            Game.Input.GhostInputMode = InputMode.MovementLimited;
            handler.ghostIsInteracting = true;
            handler.GhostInteract();
        }

        private void OnHumanInteract()
        {
            if(!humanInTrigger)
            {
                return;
            }

            Game.Input.HumanInputMode = InputMode.MovementLimited;
            handler.humanIsInteracting = true;
            handler.HumanInteract();
        }
    }
}
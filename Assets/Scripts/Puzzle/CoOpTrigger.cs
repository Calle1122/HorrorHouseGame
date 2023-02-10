using System;
using GameConstants;
using UnityEngine;
using static Puzzle.PuzzleTrigger;

namespace Puzzle
{
    public class CoOpTrigger : MonoBehaviour
    {
        [SerializeField] private CoOpTriggerHandler handler;
        [SerializeField] private bool isRightSide;
        private bool canInteract;
        private TriggerProfile triggerProfile;

        private void OnEnable()
        {
            Game.CharacterHandler.OnHumanInteract.AddListener(OnHumanInteract);
            Game.CharacterHandler.OnHumanInteract.AddListener(OnGhostInteract);
        }

        private void OnDisable()
        {
            Game.CharacterHandler.OnHumanInteract.AddListener(OnHumanInteract);
            Game.CharacterHandler.OnHumanInteract.AddListener(OnGhostInteract);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                triggerProfile = TriggerProfile.HumanTrigger;
                canInteract = true;
            }
            else if (other.CompareTag(Tags.GhostTag))
            {
                triggerProfile = TriggerProfile.GhostTrigger;
                canInteract = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (triggerProfile)
            {
                case TriggerProfile.HumanTrigger when other.CompareTag(Tags.PlayerTag):
                case TriggerProfile.GhostTrigger when other.CompareTag(Tags.GhostTag):
                    canInteract = false;
                    break;
                default:
                    Debug.LogError($"[{this}] Trigger profile has no state!", this);
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnGhostInteract()
        {
            if (!canInteract || triggerProfile != TriggerProfile.GhostTrigger)
            {
                return;
            }

            handler.humanIsRightSide = !isRightSide;
            handler.ghostIsInteracting = true;
            handler.GhostInteract();
        }

        private void OnHumanInteract()
        {
            if (!canInteract || triggerProfile != TriggerProfile.HumanTrigger)
            {
                return;
            }

            handler.humanIsRightSide = isRightSide;
            handler.humanIsInteracting = true;
            handler.HumanInteract();
        }
    }
}
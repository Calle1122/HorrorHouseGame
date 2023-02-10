using System;
using System.Collections.Generic;
using GameConstants;
using Movement;
using UnityEngine;
using static Puzzle.PuzzleTrigger;

namespace Puzzle
{
    public class CoOpTrigger : MonoBehaviour
    {
        [SerializeField] private CoOpTriggerHandler handler;
        [SerializeField] private bool isRightSide;
        private bool isInTrigger;
        public bool _triggerIsInteracting;
        private TriggerProfile triggerProfile;
        
        private List<MovementBase> _charactersInTrigger = new List<MovementBase>();

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
            if (other.CompareTag(Tags.PlayerTag))
            {
                triggerProfile = TriggerProfile.HumanTrigger;
                isInTrigger = true;
                
                var movementBase = other.GetComponent<MovementBase>();
                if (_charactersInTrigger.Contains(movementBase))
                {
                    return;
                }
                _charactersInTrigger.Add(movementBase);
            }
            else if (other.CompareTag(Tags.GhostTag))
            {
                triggerProfile = TriggerProfile.GhostTrigger;
                isInTrigger = true;
                
                var movementBase = other.GetComponent<MovementBase>();
                if (_charactersInTrigger.Contains(movementBase))
                {
                    return;
                }
                _charactersInTrigger.Add(movementBase);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!handler.canActivate)
            {
                return;
            }

            var movementBase = other.GetComponent<MovementBase>();
            if (_charactersInTrigger.Contains(movementBase))
            {
                _charactersInTrigger.Remove(movementBase);
            }

            if (_charactersInTrigger.Count == 0)
            {
                isInTrigger = false;
            }
            
            switch (triggerProfile)
            {
                case TriggerProfile.HumanTrigger when other.CompareTag(Tags.PlayerTag):
                case TriggerProfile.GhostTrigger when other.CompareTag(Tags.GhostTag):
                    isInTrigger = false;
                    break;
            }
        }

        private void OnGhostInteract()
        {
            if (!isInTrigger || triggerProfile != TriggerProfile.GhostTrigger)
            {
                return;
            }

            _triggerIsInteracting = true;
            Game.CharacterHandler.GhostInputMode = InputMode.MovementLimited;

            handler.humanIsRightSide = !isRightSide;
            handler.ghostIsInteracting = true;
            handler.GhostInteract();
        }

        private void OnHumanInteract()
        {
            if (!isInTrigger || triggerProfile != TriggerProfile.HumanTrigger)
            {
                return;
            }

            _triggerIsInteracting = true;
            Game.CharacterHandler.HumanInputMode = InputMode.MovementLimited;

            handler.humanIsRightSide = isRightSide;
            handler.humanIsInteracting = true;
            handler.HumanInteract();
        }
    }
}
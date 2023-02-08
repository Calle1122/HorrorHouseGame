using System;
using GameConstants;
using Interaction;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puzzles.PushPull
{
    public class ShelfInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private PushPullPuzzle puzzle;
        public ShelfSlot currentSlot;

        private bool isHeld;
        private bool movedThisFrame;
        private bool isEnabled;

        private void OnEnable()
        {
            Game.CharacterHandler.OnHumanMovementInput.AddListener(OnMovementInput);
            Game.CharacterHandler.OnHumanNoMovementInput.AddListener(OnMovementInput);
        }

        private void OnDisable()
        {
            Game.CharacterHandler.OnHumanMovementInput.RemoveListener(OnMovementInput);
            Game.CharacterHandler.OnHumanNoMovementInput.RemoveListener(OnMovementInput);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isEnabled)
            {
                return;
            }

            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            if (other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
                // TODO: Show input that interaction possible
                Debug.Log("Adding shelf");
                humanInteraction.AddPossibleInteractable(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            if (other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
                Debug.Log("Removing shelf");
                humanInteraction.RemovePossibleInteractable(this);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void EnableInteractable()
        {
            isEnabled = true;
        }

        public void Interact(IInteraction interaction)
        {
            if (!isEnabled)
            {
                return;
            }
            
            isHeld = true;
            Game.CharacterHandler.HumanInputMode = InputMode.InQTE;
            Debug.Log("Interacted");
        }

        public void StopInteract()
        {
            isHeld = false;
            movedThisFrame = false;
            Game.CharacterHandler.HumanInputMode = InputMode.Free;
            Debug.Log("Stopped Interaction");
            puzzle.CheckSolved();
        }

        private void OnMovementInput(Vector3 input)
        {
            if (!isHeld || !isEnabled)
            {
                return;
            }

            if (movedThisFrame && input == Vector3.zero)
            {
                movedThisFrame = false;
            }


            movedThisFrame = true;
            if (Math.Abs(input.x - 1) < 0.1f && puzzle.CanMoveShelf(this, true))
            {
                puzzle.SetNewShelfPosition(this, true);
            }

            if (Math.Abs(input.x - (-1)) < 0.1f && puzzle.CanMoveShelf(this, false))
            {
                puzzle.SetNewShelfPosition(this, false);
            }
        }

        public void UpdatePosition()
        {
            transform.position = currentSlot.transform.position;
        }

        public void DisableInteraction()
        {
            isEnabled = false;
        }
    }
}
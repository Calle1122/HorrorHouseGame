using Audio;
using GameConstants;
using Interaction;
using UnityEngine;

namespace Puzzles.PushPull
{
    public class ShelfInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private PushPullPuzzle puzzle;
        [SerializeField] private bool fromRightSide;
        [SerializeField] private Shelf shelf;
        [SerializeField] private GameObject interactSprite;
        private bool isEnabled;

        private bool isHeld;
        private bool movedThisFrame;

        public Shelf Shelf => shelf;

        private void Start()
        {
            if (interactSprite.activeSelf)
            {
                interactSprite.SetActive(false);
            }
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

            if (!other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
                return;
            }

            // TODO: Show input that interaction possible
            Debug.Log("Adding Player");
            humanInteraction.AddPossibleInteractable(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            if (other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
                // TODO: Can remove UI to show interaction isn't possible anymore here
                humanInteraction.RemovePossibleInteractable(this);
                interactSprite.SetActive(false);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Interact(IInteraction interaction)
        {
            if (!isEnabled)
            {
                return;
            }

            switch (fromRightSide)
            {
                case true when puzzle.CanMoveShelf(this, false, out var canMoveRightIndex):
                {
                    DisableInteractable();
                    switch (canMoveRightIndex)
                    {
                        case 1:
                            puzzle.SetNewShelfPosition(shelf, false);
                            break;
                        case 2:
                            puzzle.SetNewShelfPosition(shelf, true);
                            break;
                    }

                    break;
                }
                case false when puzzle.CanMoveShelf(this, true, out var canMoveLeftIndex):
                {
                    DisableInteractable();
                    switch (canMoveLeftIndex)
                    {
                        case 1:
                            puzzle.SetNewShelfPosition(shelf, true);
                            break;
                        case 2:
                            puzzle.SetNewShelfPosition(shelf, false);
                            break;
                    }

                    break;
                }
            }

            SoundManager.Instance.PlaySfx(puzzle.pushSfx);
            isHeld = true;
            Game.Input.HumanInputMode = InputMode.MovementLimited;
            UpdatePosition();
        }

        public void StopInteract()
        {
            isHeld = false;
            movedThisFrame = false;
            EnableInteractable();
            Game.Input.HumanInputMode = InputMode.Free;
            puzzle.CheckSolved();
        }

        public void ToggleUI()
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(!interactSprite.activeSelf);
            }
        }

        public void ToggleOnUI()
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(true);
            }
        }

        public void ToggleOffUI()
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(false);
            }
        }

        public void UpdatePosition()
        {
            Shelf.StartLerpPosition(this);
        }

        public void EnableInteractable()
        {
            isEnabled = true;
        }


        public void DisableInteractable()
        {
            isEnabled = false;
        }
    }
}
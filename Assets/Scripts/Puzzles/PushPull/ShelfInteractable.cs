using Audio;
using GameConstants;
using UnityEngine;

namespace Puzzles.PushPull
{
    public class ShelfInteractable : MonoBehaviour
    {
        [SerializeField] private PushPullPuzzle puzzle;
        [SerializeField] private bool fromRightSide;
        [SerializeField] private Shelf shelf;
        [SerializeField] private GameObject interactSprite;
        private bool isInteractable;

        private bool isInteracting;
        private bool isInsideTrigger;
        private bool movedThisFrame;
        private bool humanIsInside;
        private bool ghostIsInside;

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
            if (!isInteractable)
            {
                return;
            }

            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            interactSprite.SetActive(true);

            if (other.CompareTag(Tags.PlayerTag))
            {
                Game.Input.OnHumanInteract.AddListener(Interact);
                humanIsInside = true;
            }

            if (!other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            Game.Input.OnGhostInteract.AddListener(Interact);
            ghostIsInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            if (other.CompareTag(Tags.PlayerTag))
            {
                Game.Input.OnHumanInteract.RemoveListener(Interact);
                humanIsInside = false;
            }

            if (other.CompareTag(Tags.GhostTag))
            {
                Game.Input.OnGhostInteract.RemoveListener(Interact);
                ghostIsInside = false;
            }

            interactSprite.SetActive(false);
        }

        private void Interact()
        {
            if (!isInteractable || isInteracting)
            {
                return;
            }

            if (!humanIsInside && !ghostIsInside)
            {
                return;
            }

            switch (fromRightSide)
            {
                case true when puzzle.CanMoveShelf(this, false, out var canMoveRightIndex):
                {
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

            if (humanIsInside)
            {
                Game.Input.HumanInputMode = InputMode.MovementLimited;
            }

            if (ghostIsInside)
            {
                Game.Input.GhostInputMode = InputMode.MovementLimited;
            }

            SoundManager.Instance.PlaySfx(puzzle.pushSfx);
            isInteracting = true;
            DisableInteractable();
            UpdatePosition();
        }

        public void StopInteract()
        {
            isInteracting = false;
            movedThisFrame = false;
            EnableInteractable();
            Game.Input.HumanInputMode = InputMode.Free;
            Game.Input.GhostInputMode = InputMode.Free;
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
            isInteractable = true;
        }


        public void DisableInteractable()
        {
            isInteractable = false;
        }
    }
}
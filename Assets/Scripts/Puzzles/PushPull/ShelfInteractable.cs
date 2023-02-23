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
        [SerializeField] private GameObject ghostAnimationPosition;
        [SerializeField] private GameObject humanAnimationTransform;
        private bool ghostIsInside;
        private bool humanIsInside;
        private bool isInsideTrigger;
        private bool isInteractable;

        private bool isInteracting;

        public Shelf Shelf => shelf;

        public GameObject GhostAnimationPosition => ghostAnimationPosition;

        public GameObject HumanAnimationTransform => humanAnimationTransform;

        private void Start()
        {
            ToggleOffUI();
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(Interact);
            Game.Input.OnGhostInteract.RemoveListener(Interact);
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

            ToggleOnUI();

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

            if (!humanIsInside && !ghostIsInside)
            {
                ToggleOffUI();
            }
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
            ToggleOffUI();
            DisableInteractable();
            UpdatePosition();
        }

        public void StopInteract()
        {
            isInteracting = false;
            EnableInteractable();
            Game.Input.HumanInputMode = InputMode.Free;
            Game.Input.GhostInputMode = InputMode.Free;
            shelf.HumanFollowAnimation = false;
            shelf.GhostFollowAnimation = false;
            puzzle.CheckSolved();
        }

        private void ToggleOnUI()
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(true);
            }
        }

        private void ToggleOffUI()
        {
            if (interactSprite != null)
            {
                interactSprite.SetActive(false);
            }
        }

        private void UpdatePosition()
        {
            Shelf.StartLerpPosition(this, humanIsInside);
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
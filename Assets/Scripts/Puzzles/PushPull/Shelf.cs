using System;
using System.Collections;
using Animation;
using GameConstants;
using UnityEngine;

namespace Puzzles.PushPull
{
    public class Shelf : MonoBehaviour
    {
        public ShelfSlot currentSlot;
        [SerializeField] private float animationDuration;
        [SerializeField] private ShelfInteractable interactableRight;
        [SerializeField] private ShelfInteractable interactableLeft;
        private Collider shelfCollder;

        public bool HumanFollowAnimation { get; set; }
        public bool GhostFollowAnimation { get; set; }

        private void Start()
        {
            shelfCollder = GetComponent<Collider>();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        private void PlayerFollowShelf(ShelfInteractable shelfInteractable)
        {
            if (HumanFollowAnimation)
            {
                Game.Input.HumanPlayer.transform.position =
                    shelfInteractable.HumanAnimationTransform.transform.position;
            }

            if (GhostFollowAnimation)
            {
                Game.Input.GhostPlayer.transform.position = shelfInteractable.GhostAnimationPosition.transform.position;
            }
        }

        public void StartLerpPosition(ShelfInteractable shelfInteractable, bool humanInteracting)
        {
            StartCoroutine(LerpPosition(shelfInteractable, humanInteracting));
        }

        private IEnumerator PlayInteractAnimation(bool humanInteracting, ShelfInteractable shelfInteractable)
        {
            if (humanInteracting &&
                Game.Input.HumanPlayer.TryGetComponent<AnimationsHandler>(out var humanAnimationsHandler))
            {
                humanAnimationsHandler.TriggerParameter(Strings.LerpStepTrigger);
                HumanFollowAnimation = true;

                yield return StartCoroutine(LerpIntoPushPosition(Game.Input.HumanPlayer.transform, true,
                    shelfInteractable));
            }
            else if (!humanInteracting &&
                     Game.Input.GhostPlayer.TryGetComponent<AnimationsHandler>(out var ghostAnimationsHandler))
            {
                ghostAnimationsHandler.TriggerParameter(Strings.PossessTrigger);
                GhostFollowAnimation = true;

                yield return StartCoroutine(LerpIntoPushPosition(Game.Input.GhostPlayer.transform, false,
                    shelfInteractable));
            }
        }

        private IEnumerator LerpIntoPushPosition(Transform target, bool humanInteracting,
            ShelfInteractable shelfInteractable)
        {
            var currentTime = 0f;
            var lerpDuration = 1f;
            var startPos = target.position;
            var targetPos = Vector3.zero;
            if (humanInteracting)
            {
                targetPos = shelfInteractable.HumanAnimationTransform.transform.position;
            }
            else if (!humanInteracting)
            {
                targetPos = shelfInteractable.GhostAnimationPosition.transform.position;
            }

            while (currentTime < lerpDuration)
            {
                currentTime += Time.deltaTime;
                var newPos = Vector3.Lerp(startPos, targetPos, currentTime / lerpDuration);
                target.position = newPos;
                yield return null;
            }

            if (!Game.Input.HumanPlayer.TryGetComponent<AnimationsHandler>(out var humanAnimationsHandler))
            {
                yield break;
            }

            if (!humanInteracting)
            {
                yield break;
            }

            humanAnimationsHandler.TriggerParameter(Strings.PushingCabinetParam);
        }

        private IEnumerator LerpPosition(ShelfInteractable shelfInteractable, bool humanInteracting)
        {
            yield return StartCoroutine(PlayInteractAnimation(humanInteracting, shelfInteractable));
            var currentTime = 0f;
            var startPosition = transform.position;
            var targetPosition = currentSlot.transform.position;
            while (currentTime < animationDuration)
            {
                currentTime += Time.deltaTime;
                var newPosition = Vector3.Lerp(startPosition, targetPosition, currentTime / animationDuration);
                transform.position = newPosition;
                PlayerFollowShelf(shelfInteractable);
                yield return null;
            }

            if (!humanInteracting)
            {
                yield return StartCoroutine(LerpOutGhost());
            }

            if (humanInteracting)
            {
                var animationsHandler = Game.Input.HumanPlayer.GetComponent<AnimationsHandler>();
                animationsHandler.TriggerParameter(Strings.PushFinishedTrigger);
            }

            shelfInteractable.StopInteract();
        }

        private IEnumerator LerpOutGhost()
        {
            if (Game.Input.GhostPlayer.TryGetComponent<AnimationsHandler>(out var animationsHandler))
            {
                animationsHandler.TriggerParameter(Strings.UnPossessTrigger);
            }

            var currentTime = 0f;
            const float lerpDuration = 1.8f;
            var startPosition = Game.Input.GhostPlayer.transform.position;
            var targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - 1);
            while (currentTime < lerpDuration)
            {
                currentTime += Time.deltaTime;
                var newPosition = Vector3.Lerp(startPosition, targetPosition, currentTime / lerpDuration);
                Game.Input.GhostPlayer.transform.position = newPosition;
                yield return null;
            }
        }

        public void EnableInteraction()
        {
            interactableLeft.EnableInteractable();
            interactableRight.EnableInteractable();
        }

        public void DisableInteraction()
        {
            interactableLeft.DisableInteractable();
            interactableRight.DisableInteractable();
        }
    }
}
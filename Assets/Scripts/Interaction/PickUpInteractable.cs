using System.Collections;
using Animation;
using Events;
using GameConstants;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interaction
{
    [RequireComponent(typeof(Collider))]
    public class PickUpInteractable : MonoBehaviour, IInteractable
    {
        public PhantomTetherEvent OnInteract;

        [Header("Offset From Owner Transform When Held/Picked Up")] [SerializeField]
        private Vector3 followOffset;

        [SerializeField] private Transform parentTransform;
        [SerializeField] private Collider physicsCollider;
        [SerializeField] private GameObject interactSprite;

        [FormerlySerializedAs("pickUpState")] public InteractableState interactableState;
        public Transform ownerTransform;

        private Collider interactableTrigger;
        private bool isEnabled;

        private void Start()
        {
            MakeTrigger();
            if (interactSprite.activeSelf)
            {
                interactSprite.SetActive(false);
            }
        }

        private void Update()
        {
            if (ownerTransform != null && interactableState == InteractableState.Interacted)
            {
                FollowOwner();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isEnabled)
            {
                return;
            }

            if (!other.CompareTag(Tags.PlayerTag) && !other.CompareTag(Tags.GhostTag))
            {
                return;
            }

            if (other.TryGetComponent<PickupInteraction>(out var humanInteraction))
            {
                // TODO: Show input that interaction possible
                humanInteraction.AddPossibleInteractable(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) || !isEnabled || interactableState == InteractableState.Used)
            {
                return;
            }

            if (other.TryGetComponent<PickupInteraction>(out var humanInteraction))
            {
                humanInteraction.RemovePossibleInteractable(this);
                interactSprite.SetActive(false);
            }
        }

        public Transform GetTransform()
        {
            return transform != null ? transform : null;
        }

        public void StartInteract(IInteraction interaction)
        {
            StartCoroutine(StartPickupAnimation(interaction));
        }

        private void FinishInteract(IInteraction interaction)
        {
            ownerTransform = interaction.GetHandTransform();
            if (followOffset == Vector3.zero)
            {
                ToggleCollider();
            }

            interactableState = InteractableState.Interacted;
            if (OnInteract != null)
            {
                OnInteract.RaiseEvent();
            }
        }

        private IEnumerator StartPickupAnimation(IInteraction interaction)
        {
            if (!interaction.GetTransform().TryGetComponent<AnimationsHandler>(out var animationsHandler))
            {
                yield break;
            }

            Game.Input.HumanInputMode = InputMode.Limited;
            Game.Input.HumanInteractMode = InputMode.Limited;
            animationsHandler.TriggerParameter(Strings.PlacePickUpFloor);
            yield return new WaitForSeconds(2.4f);
            Game.Input.HumanInputMode = InputMode.Free;
            Game.Input.HumanInteractMode = InputMode.Free;
            FinishInteract(interaction);
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

        public void StopInteract()
        {
            ownerTransform = null;
            if (followOffset == Vector3.zero)
            {
                ToggleCollider();
            }

            interactableState = InteractableState.Free;
        }

        private void FollowOwner()
        {
            parentTransform.position = ownerTransform.position + followOffset;
        }

        private void MakeTrigger()
        {
            interactableTrigger = GetComponent<Collider>();
            isEnabled = true;
            interactableTrigger.isTrigger = true;
        }

        private void ToggleCollider()
        {
            isEnabled = !isEnabled;
            interactableTrigger.enabled = !interactableTrigger.enabled;
            if (physicsCollider != null)
            {
                physicsCollider.enabled = !physicsCollider.enabled;
            }
        }
    }

    public enum InteractableState
    {
        Free,
        Interacted,
        Used
    }
}
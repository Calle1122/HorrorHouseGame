using GameConstants;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Interaction
{
    [RequireComponent(typeof(Collider))]
    public class PickUpInteractable : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteract;

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

            if (other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
                // TODO: Show input that interaction possiblen
                humanInteraction.AddPossibleInteractable(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag) || !isEnabled || interactableState == InteractableState.Used)
            {
                return;
            }

            if (other.TryGetComponent<HumanPickupInteraction>(out var humanInteraction))
            {
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
            ownerTransform = interaction.GetTransform();
            if (followOffset == Vector3.zero)
            {
                ToggleCollider();
            }

            interactableState = InteractableState.Interacted;
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
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class HumanPickupInteraction : MonoBehaviour, IInteraction
    {
        // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
        private readonly List<IInteractable> possibleInteractables = new List<IInteractable>();
        private IInteractable heldInteractable;

        private void OnEnable()
        {
            Game.CharacterHandler.OnHumanInteract.AddListener(OnInteract);
        }

        private void OnDisable()
        {
            Game.CharacterHandler.OnHumanInteract.RemoveListener(OnInteract);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void AddPossibleInteractable(IInteractable pickUpInteractable)
        {
            if (possibleInteractables.Contains(pickUpInteractable))
            {
                return;
            }

            possibleInteractables.Add(pickUpInteractable);
        }

        public void RemovePossibleInteractable(IInteractable pickUpInteractable)
        {
            if (!possibleInteractables.Contains(pickUpInteractable))
            {
                return;
            }

            possibleInteractables.Remove(pickUpInteractable);
        }

        public void OnInteract()
        {
            if (heldInteractable != null)
            {
                StopInteract();
                return;
            }

            InteractClosestInteractable();
        }

        public void StopInteract()
        {
            heldInteractable.StopInteract();
            heldInteractable = null;
        }

        private void InteractClosestInteractable()
        {
            if (possibleInteractables.Count <= 0)
            {
                return;
            }

            var closestInteractable = possibleInteractables[0];
            var closestDistance = float.MaxValue;
            foreach (var interactable in possibleInteractables)
            {
                if (closestInteractable == null)
                {
                    closestInteractable = interactable;
                }

                var currentDistance = Vector3.Distance(transform.position, interactable.GetTransform().position);

                if (!(currentDistance < closestDistance))
                {
                    continue;
                }

                closestInteractable = interactable;
                closestDistance = currentDistance;
            }

            heldInteractable = closestInteractable;
            closestInteractable.Interact(this);
        }
    }
}
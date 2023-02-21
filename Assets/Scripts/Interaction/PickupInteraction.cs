using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class PickupInteraction : MonoBehaviour, IInteraction
    {
        private readonly List<IInteractable> possibleInteractables = new List<IInteractable>();
        private IInteractable indicatedInteractable;
        private IInteractable closestInteractable;
        private IInteractable heldInteractable;

        private void OnEnable()
        {
            Game.Input.OnHumanInteract.AddListener(OnInteract);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(OnInteract);
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
            UpdateClosestInteractable();
        }

        public void RemovePossibleInteractable(IInteractable pickUpInteractable)
        {
            if (!possibleInteractables.Contains(pickUpInteractable))
            {
                return;
            }

            possibleInteractables.Remove(pickUpInteractable);
            UpdateClosestInteractable();
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

            heldInteractable = closestInteractable;
            closestInteractable.Interact(this);
        }

        private void UpdateClosestInteractable()
        {
            var closestDistance = float.MaxValue;
            foreach (var interactable in possibleInteractables)
            {
                closestInteractable ??= interactable;

                var currentDistance = Vector3.Distance(transform.position, interactable.GetTransform().position);

                if (!(currentDistance < closestDistance))
                {
                    continue;
                }

                closestInteractable = interactable;
                closestDistance = currentDistance;
            }

            closestInteractable.ToggleOnUI();
            if (indicatedInteractable != closestInteractable)
            {
                indicatedInteractable?.ToggleOffUI();
            }

            indicatedInteractable = closestInteractable;
        }
    }
}
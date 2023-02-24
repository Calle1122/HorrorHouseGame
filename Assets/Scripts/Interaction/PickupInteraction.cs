using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interaction
{
    public class PickupInteraction : MonoBehaviour, IInteraction
    {
        [SerializeField] private Transform handTransform;

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

        public Transform GetHandTransform()
        {
            return handTransform;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void AddPossibleInteractable(IInteractable interactable)
        {
            if (possibleInteractables.Contains(interactable))
            {
                return;
            }

            possibleInteractables.Add(interactable);
            UpdateClosestInteractable();
        }

        public void RemovePossibleInteractable(IInteractable interactable)
        {
            if (!possibleInteractables.Contains(interactable))
            {
                return;
            }

            possibleInteractables.Remove(interactable);
            UpdateClosestInteractable();
        }

        public void OnInteract()
        {
            if (heldInteractable != null)
            {
                heldInteractable.ToggleOnUI();
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
            heldInteractable.ToggleOffUI();
            closestInteractable.StartInteract(this);
        }

        private void UpdateClosestInteractable()
        {
            var closestDistance = float.MaxValue;
            foreach (var interactable in possibleInteractables.Where(interactable => interactable == null))
            {
                possibleInteractables.Remove(interactable);
            }

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
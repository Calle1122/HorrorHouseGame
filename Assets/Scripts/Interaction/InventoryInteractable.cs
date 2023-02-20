using System;
using Events;
using GameConstants;
using UnityEngine;

namespace Interaction
{
    public class InventoryInteractable : MonoBehaviour
    {
        [SerializeField] private PhantomTetherEvent onPickupEvent;
        [SerializeField] private GameObject interactableUI;
        private bool isInTrigger;

        private void OnEnable()
        {
            Game.Input.OnHumanInteract.AddListener(OnInteract);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(OnInteract);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            isInTrigger = true;
            interactableUI.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            isInTrigger = false;
            interactableUI.SetActive(false);
        }

        private void OnInteract()
        {
            if (!isInTrigger)
            {
                return;
            }

            interactableUI.SetActive(false);
            Game.Input.OnHumanInteract.RemoveListener(OnInteract);
            onPickupEvent.RaiseEvent();
        }
    }
}
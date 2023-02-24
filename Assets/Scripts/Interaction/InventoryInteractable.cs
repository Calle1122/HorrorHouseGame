using System.Collections;
using Animation;
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

            StartCoroutine(StartPickUpAnimations());
        }

        private IEnumerator StartPickUpAnimations()
        {
            if (Game.Input.HumanPlayer.TryGetComponent<AnimationsHandler>(out var animationsHandler))
            {
                animationsHandler.TriggerParameter(Strings.PlacePickUpFloor);
            }

            Game.Input.HumanInputMode = InputMode.MovementLimited;
            yield return new WaitForSeconds(2.4f);
            interactableUI.SetActive(false);
            Game.Input.OnHumanInteract.RemoveListener(OnInteract);
            onPickupEvent.RaiseEvent();
            Game.Input.HumanInputMode = InputMode.Free;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Animation;
using Audio;
using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class PlaceItemTrigger : MonoBehaviour
    {
        [SerializeField] private RitualManager ritMan;
        [SerializeField] private RitualInventory ritInv;
        [SerializeField] private int itemIndex;
        [SerializeField] private GameObject ritualItemInWorld;
        [SerializeField] private GameObject interactSprite;

        [SerializeField] private List<DialogueSo> dialogueToQueue;

        private bool canActivate;

        private void Start()
        {
            if (interactSprite.activeSelf)
            {
                interactSprite.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Game.Input.OnHumanInteract.AddListener(PlaceItem);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(PlaceItem);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            canActivate = true;
            ToggleInteractableUI();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.PlayerTag))
            {
                return;
            }

            canActivate = false;
            ToggleInteractableUI();
        }

        private void PlaceItem()
        {
            if (!canActivate)
            {
                return;
            }

            if (!ritInv.CheckForItem(itemIndex))
            {
                return;
            }

            foreach (DialogueSo dialogue in dialogueToQueue)
            {
                DialogueCanvas.Instance.QueueDialogue(dialogue);
            }
            StartCoroutine(StartPlacingAnimation());
        }

        private IEnumerator StartPlacingAnimation()
        {
            Game.Input.HumanInputMode = InputMode.Limited;
            Game.Input.HumanPlayer.GetComponent<AnimationsHandler>().TriggerParameter(Strings.PlacePickUpFloor);
            yield return new WaitForSeconds(2.4f);
            Game.Input.HumanInputMode = InputMode.Free;
            DialogueCanvas.Instance.LockRitualItem(itemIndex);
            ritualItemInWorld.SetActive(true);
            ritMan.PlaceItem(itemIndex);
            Destroy(gameObject);
        }

        private void ToggleInteractableUI()
        {
            interactSprite.SetActive(!interactSprite.activeSelf);
        }
    }
}
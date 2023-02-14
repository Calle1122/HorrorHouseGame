using System.Collections;
using Audio;
using Events;
using Lakeview_Interactive.QTE_System.Scripts.QTEs;
using QTESystem;
using UnityEngine;

namespace Puzzle
{
    public class CoOpTriggerHandler : MonoBehaviour
    {
        public DefaultEvent eventToRaise;
        [HideInInspector] public bool ghostIsInteracting, humanIsInteracting;

        [SerializeField] private GameObject qteObjectRight;
        [SerializeField] private GameObject qteObjectLeft;
        public bool canActivate = true;
        [SerializeField] private DialogueSo waitingDialogue;

        private int _successCounter;

        public void GhostInteract()
        {
            if (!canActivate || !ghostIsInteracting)
            {
                return;
            }

            StartCoroutine(TimedDialogue());
            CheckToStartQte();
        }

        public void HumanInteract()
        {
            if (!canActivate || !humanIsInteracting)
            {
                return;
            }

            StartCoroutine(TimedDialogue());
            CheckToStartQte();
        }

        private void CheckToStartQte()
        {
            if (!ghostIsInteracting || !humanIsInteracting || !canActivate)
            {
                return;
            }

            canActivate = false;

            qteObjectLeft.SetActive(true);
            qteObjectRight.SetActive(true);
            qteObjectRight.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Human);
            qteObjectLeft.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Ghost);
        }

        public void ResetQteTimer(float secondsToWait)
        {
            StartCoroutine(QteCooldown(secondsToWait));
            EnableMovementInput();
        }

        public void EnableMovementInput()
        {
            Game.CharacterHandler.HumanInputMode = InputMode.Free;
            Game.CharacterHandler.GhostInputMode = InputMode.Free;
        }

        private IEnumerator QteCooldown(float secondsToWait)
        {
            humanIsInteracting = false;
            ghostIsInteracting = false;
            _successCounter = 0;

            qteObjectRight.SetActive(false);
            qteObjectLeft.SetActive(false);

            yield return new WaitForSeconds(secondsToWait);

            canActivate = true;
        }

        private void DisableTrigger()
        {
            Game.CharacterHandler.OnHumanInteract.RemoveListener(HumanInteract);
            Game.CharacterHandler.OnGhostInteract.RemoveListener(GhostInteract);
            gameObject.SetActive(false);
        }

        public void AddSuccess()
        {
            _successCounter++;
            if (_successCounter == 2)
            {
                CompleteEvent();
            }
        }

        public void CompleteEvent()
        {
            EnableMovementInput();
            canActivate = false;
            eventToRaise.RaiseEvent();
            DisableTrigger();
        }

        private IEnumerator TimedDialogue()
        {
            yield return new WaitForSeconds(5f);

            if (ghostIsInteracting && !humanIsInteracting)
            {
                DialogueCanvas.Instance.QueueDialogue(waitingDialogue);
            }

            if (humanIsInteracting && !ghostIsInteracting)
            {
                DialogueCanvas.Instance.QueueDialogue(waitingDialogue);
            }
        }
    }
}
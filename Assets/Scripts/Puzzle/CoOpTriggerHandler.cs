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
        public PhantomTetherEvent eventToRaise;
        [HideInInspector] public bool ghostIsInteracting, humanIsInteracting;

        [SerializeField] private QTEHandler qteHandlerRight;
        [SerializeField] private QTEHandler qteHandlerLeft;
        [SerializeField] private QTEHandler qteHandlerAlternating;
        public bool canActivate = true;
        [SerializeField] private DialogueSo waitingDialogue;

        private int successCounter;

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

            if (qteHandlerAlternating != null)
            {
                qteHandlerAlternating.gameObject.SetActive(true);
                qteHandlerAlternating.QTEComponent.Reset();
            }
            else
            {
                qteHandlerLeft.gameObject.SetActive(true);
                qteHandlerRight.gameObject.SetActive(true);
                qteHandlerRight.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Human);
                qteHandlerLeft.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Ghost);
            }
        }

        public void ResetQteTimer(float secondsToWait)
        {
            StartCoroutine(QteCooldown(secondsToWait));
            EnableMovementInput();
        }

        public void EnableMovementInput()
        {
            Game.Input.HumanInputMode = InputMode.Free;
            Game.Input.GhostInputMode = InputMode.Free;
        }

        private IEnumerator QteCooldown(float secondsToWait)
        {
            humanIsInteracting = false;
            ghostIsInteracting = false;
            successCounter = 0;

            qteHandlerRight.gameObject.SetActive(false);
            qteHandlerLeft.gameObject.SetActive(false);

            yield return new WaitForSeconds(secondsToWait);

            canActivate = true;
        }

        private void DisableTrigger()
        {
            Game.Input.OnHumanInteract.RemoveListener(HumanInteract);
            Game.Input.OnGhostInteract.RemoveListener(GhostInteract);
            gameObject.SetActive(false);
        }

        public void AddSuccess()
        {
            successCounter++;
            if (successCounter == 2)
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
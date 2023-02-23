using System.Collections;
using Audio;
using Events;
using UnityEngine;

namespace Puzzle
{
    public class CoOpTriggerHandler : MonoBehaviour
    {
        public PhantomTetherEvent eventToRaise;

        [HideInInspector] public bool ghostIsInteracting, humanIsInteracting;
        [SerializeField] private QteHandler qteHandlerRight;
        [SerializeField] private QteHandler qteHandlerLeft;
        [SerializeField] private QteHandler qteHandlerAlternating;
        [SerializeField] private DialogueSo waitingDialogue;

        private bool canActivate = true;
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

            // START CO-OP QTE
            canActivate = false;
            if (qteHandlerAlternating != null)
            {
                // Start Alternating COOP QTE
                qteHandlerAlternating.gameObject.SetActive(true);
                qteHandlerAlternating.QteComponent.Reset();
                StartCoroutine(qteHandlerAlternating.StartCoopQteAnimations());
            }
            else
            {
                // Start 2 Mashing QTEs
                qteHandlerLeft.gameObject.SetActive(true);
                qteHandlerRight.gameObject.SetActive(true);
                StartCoroutine(qteHandlerRight.StartSingleQteAnimation(true));
                StartCoroutine(qteHandlerLeft.StartSingleQteAnimation(false));
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
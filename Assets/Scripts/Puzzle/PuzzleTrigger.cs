using System.Collections;
using Lakeview_Interactive.QTE_System.Scripts.QTEs;
using QTESystem;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleTrigger : MonoBehaviour
    {
        public enum TriggerProfile
        {
            GhostTrigger,
            HumanTrigger
        }

        public TriggerProfile thisTriggerProfile;

        [SerializeField] private GameObject qteObject;
        [SerializeField] private bool canActivate;
        private bool isHuman;

        private void OnEnable()
        {
            switch (thisTriggerProfile)
            {
                case TriggerProfile.GhostTrigger:
                    isHuman = false;
                    Game.CharacterHandler.OnGhostInteract.AddListener(EnableQte);
                    break;
                case TriggerProfile.HumanTrigger:
                    isHuman = true;
                    Game.CharacterHandler.OnHumanInteract.AddListener(EnableQte);
                    break;
                default:
                    Debug.LogError("Incorrect trigger profile", this);
                    break;
            }
        }

        private void OnDisable()
        {
            switch (thisTriggerProfile)
            {
                case TriggerProfile.GhostTrigger:
                    Game.CharacterHandler.OnGhostInteract.RemoveListener(EnableQte);
                    break;
                case TriggerProfile.HumanTrigger:
                    Game.CharacterHandler.OnHumanInteract.RemoveListener(EnableQte);
                    break;
                default:
                    Debug.LogError("Incorrect trigger profile", this);
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            canActivate = true;
        }

        private void OnTriggerExit(Collider other)
        {
            canActivate = false;
        }

        private void EnableQte()
        {
            if (!canActivate)
            {
                return;
            }

            canActivate = false;
            qteObject.SetActive(true);

            switch (isHuman)
            {
                case true:
                    Game.CharacterHandler.HumanInputMode = InputMode.MovementLimited;
                    qteObject.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Human);
                    break;
                case false:
                    Game.CharacterHandler.GhostInputMode = InputMode.MovementLimited;
                    qteObject.GetComponentInChildren<MashingQTE>().SetCharType(CharacterType.Ghost);
                    break;
            }
        }

        public void ResetQteTimer(float secondsToWait)
        {
            StartCoroutine(QteCooldown(secondsToWait));
            EnableMovementInput();
        }

        public void EnableMovementInput()
        {
            switch (isHuman)
            {
                case true:
                    Game.CharacterHandler.HumanInputMode = InputMode.Free;
                    break;
                case false:
                    Game.CharacterHandler.GhostInputMode = InputMode.Free;
                    break;
            }
        }

        private IEnumerator QteCooldown(float secondsToWait)
        {
            qteObject.SetActive(false);

            yield return new WaitForSeconds(secondsToWait);

            canActivate = true;
        }

        public void DestroyTrigger()
        {
            Game.CharacterHandler.OnGhostInteract.RemoveListener(EnableQte);
            Destroy(gameObject);
        }
    }
}
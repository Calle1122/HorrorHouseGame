using System.Collections;
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
        [SerializeField] private bool canActivate = false;

        private void OnEnable()
        {
            switch (thisTriggerProfile)
            {
                case TriggerProfile.GhostTrigger:
                    Game.CharacterHandler.OnGhostInteract.AddListener(EnableQte);
                    break;
                case TriggerProfile.HumanTrigger:
                    Game.CharacterHandler.OnHumanInteract.AddListener(EnableQte);
                    break;
                default:
                    Debug.Log("Incorrect trigger profile", this);
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
                    Debug.Log("Incorrect trigger profile", this);
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
            if (canActivate)
            {
                canActivate = false;
                qteObject.SetActive(true);
            }
        }

        public void ResetQteTimer(float secondsToWait)
        {
            StartCoroutine(QteCooldown(secondsToWait));
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

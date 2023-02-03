using System.Collections;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject qteObject;
        
        [SerializeField] private bool canActivate = false;

        private void Awake()
        {
            Game.CharacterHandler.OnGhostInteract.AddListener(EnableQte);
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

using System.Collections;
using Animation;
using Events;
using GameConstants;
using UnityEngine;

namespace Puzzle
{
    public class RitualManager : MonoBehaviour
    {
        public bool item1Placed, item2Placed, item3Placed;
        [SerializeField] private PhantomTetherEvent startRitualEvent;
        [SerializeField] private PhantomTetherEvent ritualFinishedEvent;

        [SerializeField] private Transform ghostLerpPos, humanLerpPos;

        [SerializeField] private float ritualTime;

        public void PlaceItem(int itemIndex)
        {
            switch (itemIndex)
            {
                case 1:
                    item1Placed = true;
                    break;
                case 2:
                    item2Placed = true;
                    break;
                case 3:
                    item3Placed = true;
                    break;
            }
            
            CheckToStartRitual();
        }
        
        private void CheckToStartRitual()
        {
            if (!item1Placed || !item2Placed || !item3Placed)
            {
                return;
            }
            
            startRitualEvent.RaiseEvent();

            Game.Input.GhostInputMode = InputMode.Limited;
            Game.Input.HumanInputMode = InputMode.Limited;
            
            StartCoroutine(LerpCharacters());
            StartCoroutine(RitualTimer());
        }

        private IEnumerator RitualTimer()
        {
            yield return new WaitForSeconds(ritualTime);

            ritualFinishedEvent.RaiseEvent();
        }

        private IEnumerator LerpCharacters()
        {
            var currentTime = 0f;
            const float lerpDuration = 1f;
            var startPositionGhost = Game.Input.GhostPlayer.transform.position;
            var startPositionHuman = Game.Input.HumanPlayer.transform.position;

            var positionGhost = ghostLerpPos.position;
            var ghostTargetPos = new Vector3(positionGhost.x, startPositionGhost.y, positionGhost.z);
            
            var positionHuman = humanLerpPos.position;
            var humanTargetPos = new Vector3(positionHuman.x, startPositionHuman.y, positionHuman.z);
            
            while (currentTime < lerpDuration)
            {
                currentTime += Time.deltaTime;
                var newGhostPosition = Vector3.Lerp(startPositionGhost, ghostTargetPos, currentTime / lerpDuration);
                var newHumanPosition = Vector3.Lerp(startPositionHuman, humanTargetPos, currentTime / lerpDuration);
                Game.Input.GhostPlayer.transform.position = newGhostPosition;
                Game.Input.HumanPlayer.transform.position = newHumanPosition;
                yield return null;
            }
            
            Game.Input.HumanPlayer.GetComponent<AnimationsHandler>().TriggerParameter(Strings.PumpKickTriggerParameter);
            Game.Input.GhostPlayer.GetComponent<AnimationsHandler>().TriggerParameter(Strings.StompTrigger);
        }
    }
}
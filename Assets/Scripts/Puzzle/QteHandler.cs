using System.Collections;
using Animation;
using GameConstants;
using Lakeview_Interactive.QTE_System.Scripts.QTEs;
using QTESystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle
{
    public class QteHandler : MonoBehaviour
    {
        [SerializeField] private QTE qteComponent;

        [FormerlySerializedAs("ghostPosition")] [SerializeField]
        private GameObject ghostObject;

        [FormerlySerializedAs("humanPosition")] [SerializeField]
        private GameObject humanObject;

        [SerializeField] private bool playPushAnimation;

        public QTE QteComponent { get; private set; }

        private void Awake()
        {
            QteComponent = qteComponent;
        }

        public IEnumerator StartSingleQteAnimation(bool isHuman)
        {
            if (isHuman)
            {
                Game.Input.HumanInputMode = InputMode.MovementLimited;
                qteComponent.SetCharType(CharacterType.Human);
                yield return StartCoroutine(LerpPosition(humanObject.transform.position,
                    Game.Input.HumanPlayer.transform, Strings.LerpStepTrigger));
            }
            else if (!isHuman)
            {
                Game.Input.GhostInputMode = InputMode.MovementLimited;
                qteComponent.SetCharType(CharacterType.Ghost);
                yield return StartCoroutine(LerpPosition(ghostObject.transform.position,
                    Game.Input.GhostPlayer.transform, Strings.PossessTrigger));
            }

            EnableQTE();
            qteComponent.BeginQTE();
        }

        public void StopQteAnimation()
        {
            if (Game.Input.HumanPlayer.TryGetComponent<AnimationsHandler>(out var animationsHandler))
            {
                animationsHandler.SetBoolParameter(Strings.PushingCouchParam, false);
                animationsHandler.TriggerParameter(Strings.PushFinishedTrigger);
            }
        }

        public IEnumerator StartCoopQteAnimations()
        {
            Game.Input.HumanInputMode = InputMode.MovementLimited;
            Game.Input.GhostInputMode = InputMode.MovementLimited;
            if (playPushAnimation)
            {
                if (Game.Input.HumanPlayer.TryGetComponent<AnimationsHandler>(out var humanAnimHandler))
                {
                    humanAnimHandler.SetBoolParameter(Strings.PushingCouchParam, true);
                }
            }

            yield return StartCoroutine(LerpBothPlayers());
            EnableQTE();
            qteComponent.BeginQTE();
        }

        private IEnumerator LerpBothPlayers()
        {
            PlayLerpAnimation(Game.Input.HumanPlayer.transform, Strings.LerpStepTrigger);
            PlayLerpAnimation(Game.Input.GhostPlayer.transform, Strings.PossessTrigger);

            const float lerpDuration = 1f;
            var currentTime = 0f;
            var startPosHuman = Game.Input.HumanPlayer.transform.position;
            var humanPosition = humanObject.transform.position;
            var humanTargetPos = new Vector3(humanPosition.x, startPosHuman.y, humanPosition.z);
            while (currentTime < lerpDuration)
            {
                currentTime += Time.deltaTime;

                var humanNewPos = Vector3.Lerp(startPosHuman, humanTargetPos, currentTime / lerpDuration);
                var ghostNewPos = Vector3.Lerp(Game.Input.GhostPlayer.transform.position,
                    ghostObject.transform.position, currentTime / lerpDuration);

                Game.Input.HumanPlayer.transform.position = humanNewPos;
                Game.Input.GhostPlayer.transform.position = ghostNewPos;
                yield return null;
            }
        }

        private IEnumerator LerpPosition(Vector3 targetPosition, Transform targetTransform, string triggerParameter)
        {
            PlayLerpAnimation(targetTransform, triggerParameter);

            const float lerpDuration = 1f;
            var currentTime = 0f;
            var startPos = targetTransform.position;
            var targetXZPos = new Vector3(targetPosition.x, startPos.y, targetPosition.z);
            
            while (currentTime < lerpDuration)
            {
                currentTime += Time.deltaTime;
                var newPos = Vector3.Lerp(startPos, targetXZPos, currentTime / lerpDuration);
                targetTransform.transform.position = newPos;
                yield return null;
            }
        }

        private static void PlayLerpAnimation(Component targetTransform, string triggerParameter)
        {
            if (targetTransform.TryGetComponent<AnimationsHandler>(out var humanAnimHandler))
            {
                humanAnimHandler.TriggerParameter(triggerParameter);
            }
            else if (targetTransform.TryGetComponent<AnimationsHandler>(out var ghostAnimHandler))
            {
                ghostAnimHandler.TriggerParameter(triggerParameter);
            }
        }

        public void EnableQTE()
        {
            qteComponent.gameObject.SetActive(true);
        }
    }
}
using UnityEngine;

namespace Animation
{
    public class AnimationsHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetBoolParameter(string boolName, bool state)
        {
            animator.SetBool(boolName, state);
        }

        public void TriggerParameter(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}
using GameConstants;
using UnityEngine;

namespace Animation
{
    public class AnimationsHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetBool(string name, bool state)
        {
            animator.SetBool(name, state);
        }

        public void TriggerJump()
        {
            animator.SetTrigger(Strings.JumpTriggerParam);
        }
    }
}
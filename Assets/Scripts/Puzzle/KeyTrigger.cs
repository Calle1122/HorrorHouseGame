using Events;
using UnityEngine;

namespace Puzzle
{
    public class KeyTrigger : MonoBehaviour
    {
        public DefaultEvent eventToRaise;

        [SerializeField] private Collider keyObjectCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (other == keyObjectCollider)
            {
                eventToRaise.RaiseEvent();
            }
        }
    }
}
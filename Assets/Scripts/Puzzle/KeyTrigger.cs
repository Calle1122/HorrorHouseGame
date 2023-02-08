using Events;
using UnityEngine;

namespace Puzzle
{
    public class KeyTrigger : MonoBehaviour
    {
        public DefaultEvent eventToRaise;

        [SerializeField] private GameObject key;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == key)
            {
                eventToRaise.RaiseEvent();
            }
        }
    }
}
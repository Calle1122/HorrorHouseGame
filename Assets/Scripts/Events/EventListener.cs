using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class EventListener : MonoBehaviour
    {
        public PhantomTetherEvent @event;
        [SerializeField] private UnityEvent response;

        private void OnEnable()
        {
            @event.RegisterListener(this);
        }

        private void OnDisable()
        {
            @event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}
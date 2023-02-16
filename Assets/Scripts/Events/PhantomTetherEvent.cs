using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "DefaultEvent", menuName = "EventBase/DefaultEvent")]
    public class PhantomTetherEvent : ScriptableObject, IEventBase
    {
        private List<EventListener> listeners = new();

        public void RaiseEvent()
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised();
            }
        }

        public void RegisterListener(EventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(EventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
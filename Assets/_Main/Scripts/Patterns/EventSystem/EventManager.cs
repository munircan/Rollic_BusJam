using System.Collections.Generic;
using _Main.Patterns.Singleton;
using UnityEngine;

namespace _Main.Patterns.EventSystem
{
    public delegate void CustomEventAction<TCustomEvent>(TCustomEvent customEvent) where TCustomEvent : ICustomEvent;

    public static class EventManager
    {
        public static void Publish<TCustomEvent>(TCustomEvent customEvent) where TCustomEvent : ICustomEvent
        {
            EventList<TCustomEvent>.Instance.Publish(customEvent);
        }

        public static void Subscribe<TCustomEvent>(CustomEventAction<TCustomEvent> action) where TCustomEvent : ICustomEvent
        {
            EventList<TCustomEvent>.Instance.Subscribe(action);
        }

        public static void Unsubscribe<TCustomEvent>(CustomEventAction<TCustomEvent> action) where TCustomEvent : ICustomEvent
        {
            EventList<TCustomEvent>.Instance.Unsubscribe(action);
        }
    }

    public class EventList<TCustomEvent> : Singleton<EventList<TCustomEvent>> where TCustomEvent : ICustomEvent
    {
        private List<CustomEventAction<TCustomEvent>> _customEventActions = new List<CustomEventAction<TCustomEvent>>();

        public void Publish(TCustomEvent customEvent)
        {
            var count = _customEventActions.Count;
            for (int i = 0; i < count; i++)
            {
                _customEventActions[i]?.Invoke(customEvent);
            }
        }

        public void Subscribe(CustomEventAction<TCustomEvent> action)
        {
            if (_customEventActions.Contains(action))
            {
                return;
            }

            _customEventActions.Add(action);
        }

        public void Unsubscribe(CustomEventAction<TCustomEvent> action)
        {
            if (!_customEventActions.Contains(action))
            {
                Debug.LogError("Event that you trying to unsubscribe is not subscribed! | Action Name: " +
                               action.Method.Name);
                return;
            }

            _customEventActions.Remove(action);
        }
    }
}
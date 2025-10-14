using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    public class EventBusChanged : ICustomEvent
    {
        public Bus Bus;

        public static EventBusChanged Create(Bus bus)
        {
            var eventBusChanged = new EventBusChanged();
            eventBusChanged.Bus = bus;
            return eventBusChanged;
        }
    }
}
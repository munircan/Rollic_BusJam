using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.BusSystem.Components;

namespace _Main.Scripts.GamePlay.CustomEvents
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
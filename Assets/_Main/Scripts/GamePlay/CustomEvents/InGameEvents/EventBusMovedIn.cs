using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.BusSystem.Components;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    public class EventBusMovedIn : ICustomEvent
    {
        public Bus Bus  { get; set; }

        public static EventBusMovedIn Create(Bus bus)
        {
            var eventBusMovedIn = new EventBusMovedIn();
            eventBusMovedIn.Bus = bus;
            return eventBusMovedIn;
        }
    }
}
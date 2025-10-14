using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    public class EventBusMovedIn : ICustomEvent
    {
        public Bus Bus  { get; set; }
        
        public bool IsInstant { get; set; }

        public static EventBusMovedIn Create(Bus bus, bool  isInstant)
        {
            var eventBusMovedIn = new EventBusMovedIn();
            eventBusMovedIn.Bus = bus;
            eventBusMovedIn.IsInstant = isInstant;
            return eventBusMovedIn;
        }
    }
}
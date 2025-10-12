using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventLevelFail : ICustomEvent
    {
        // ADJUST LATER
        public int ClickCount { get; set; }

        public static EventLevelFail Create()
        {
            var eventLevelFail = new EventLevelFail();
            return eventLevelFail;
        }
    }
}
using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventLevelSuccess : ICustomEvent
    {
        // ADJUST LATER
        public int ClickCount { get; set; }

        public static EventLevelSuccess Create()
        {
            var eventLevelSuccess = new EventLevelSuccess();
            return eventLevelSuccess;
        }
    }
}
using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.LevelEvents
{
    public class EventLevelLoaded : ICustomEvent
    {
        public static EventLevelLoaded Create()
        {
            return new EventLevelLoaded();
        }
    }
}
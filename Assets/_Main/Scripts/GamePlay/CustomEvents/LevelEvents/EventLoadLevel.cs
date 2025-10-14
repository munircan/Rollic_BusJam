using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.LevelEvents
{
    public class EventLoadLevel : ICustomEvent
    {
        public static EventLoadLevel Create()
        {
            return new EventLoadLevel();
        }
    }
}
using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventLoadLevel : ICustomEvent
    {
        public static EventLoadLevel Create()
        {
            return new EventLoadLevel();
        }
    }
}
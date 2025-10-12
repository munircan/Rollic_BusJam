using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventLevelLoad : ICustomEvent
    {
        public static EventLevelLoad Create()
        {
            return new EventLevelLoad();
        }
    }
}
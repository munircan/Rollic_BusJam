using _Main.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventPersonGetIntoBus : ICustomEvent
    {
        public static EventPersonGetIntoBus Create()
        {
            return new EventPersonGetIntoBus();
        }
    }
}
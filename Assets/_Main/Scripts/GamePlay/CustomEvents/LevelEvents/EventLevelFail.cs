using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.Utilities;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    public class EventLevelFail : ICustomEvent
    {
        // ADJUST LATER
        public int ClickCount { get; set; }

        public static EventLevelFail Create(int clickCount)
        {
            GameConfig.State = GameState.Fail;
            var eventLevelFail = new EventLevelFail();
            eventLevelFail.ClickCount = clickCount;
            return eventLevelFail;
        }
    }
}
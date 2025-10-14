using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.GamePlay.Utilities;

namespace _Main.Scripts.GamePlay.CustomEvents.LevelEvents
{
    public class EventLevelFail : ICustomEvent
    {
        public int ClickCount { get; set; }
        
        public FailReason FailReason { get; set; }

        public static EventLevelFail Create(int clickCount,FailReason failReason = FailReason.Default)
        {
            GameConfig.State = GameState.Fail;
            var eventLevelFail = new EventLevelFail();
            eventLevelFail.ClickCount = clickCount;
            eventLevelFail.FailReason = failReason;
            return eventLevelFail;
        }
    }
}
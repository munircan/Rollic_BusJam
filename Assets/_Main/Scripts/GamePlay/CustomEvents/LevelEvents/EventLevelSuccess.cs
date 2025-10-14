using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.Utilities;

namespace _Main.Scripts.GamePlay.CustomEvents.LevelEvents
{
    public class EventLevelSuccess : ICustomEvent
    {
        public int ClickCount { get; set; }

        public static EventLevelSuccess Create(int clickCount)
        {
            GameConfig.State = GameState.Success;
            var eventLevelSuccess = new EventLevelSuccess();
            eventLevelSuccess.ClickCount = clickCount;
            return eventLevelSuccess;
        }
    }
}
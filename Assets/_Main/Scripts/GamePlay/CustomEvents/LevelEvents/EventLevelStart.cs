using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.GamePlay.Utilities;

namespace _Main.Scripts.GamePlay.CustomEvents.LevelEvents
{
    public class EventLevelStart : ICustomEvent
    {
        public static EventLevelStart Create()
        {
            GameConfig.State = GameState.Playing;
            return new EventLevelStart();
        }
    }
}
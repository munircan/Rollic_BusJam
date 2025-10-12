using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.Utilities;

namespace _Main.Scripts.GamePlay.CustomEvents
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
using _Main.GamePlay.TileSystem;
using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.TileSystem.Components;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    public class EventTileChanged : ICustomEvent
    {
        public Tile Tile { get; set; }
        public static EventTileChanged Create(Tile tile)
        {
            var eventTileChanged = new EventTileChanged
            {
                Tile = tile
            };
            return eventTileChanged;
        }
    }
}
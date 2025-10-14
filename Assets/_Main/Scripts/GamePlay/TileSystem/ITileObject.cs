using _Main.Scripts.GamePlay.TileSystem.Components;

namespace _Main.Scripts.GamePlay.TileSystem
{
    public interface ITileObject
    {
        public Tile Tile { get; set; }

        public void Execute();
    }
}
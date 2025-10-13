using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.SlotSystem;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    public static class LevelDataHelper
    {
        public static TileData GetTile(this LevelData levelData ,int x, int y)
        {
            return levelData.Tiles[x * levelData.TileWidth + y];
        }
        public static SlotData GetSlot(this LevelData levelData ,int x, int y)
        {
            return levelData.Slots[x * levelData.SlotWidth + y];
        }
    }
}
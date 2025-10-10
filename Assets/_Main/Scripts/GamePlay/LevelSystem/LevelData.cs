using System;
using _Main.GamePlay.TileSystem;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [Serializable]
    public class LevelData
    {
        public int TileWidth = 5;
        public int TileHeight = 5;
        public TileData[]  Tiles;

        private TileData[,] _tilesData;
        
        public void Initialize()
        {
            InitializeTiles();
        }

        private void InitializeTiles()
        {
            if (Tiles == null || Tiles.Length != TileWidth * TileHeight)
            {
                Tiles = new TileData[TileWidth * TileHeight];
            }

            _tilesData = new TileData[TileHeight, TileWidth];

            for (int x = 0; x < TileHeight; x++)
            {
                for (int y = 0; y < TileWidth; y++)
                {
                    _tilesData[x, y] = Tiles[x * TileWidth + y];
                }
            }
        }
        
        public TileData GetTile(int x, int y) => _tilesData[x, y];
        
        
    }
}
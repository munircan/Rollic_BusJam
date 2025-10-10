using System;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [Serializable]
    public class LevelData
    {
        public BusData[] Buses;
        
        public int SlotWidth = 5;
        public int SlotHeight = 1;
        public SlotData[] Slots;
        
        private SlotData[,] _slotsData;
        
        
        public int TileWidth = 5;
        public int TileHeight = 5;
        public TileData[]  Tiles;

        private TileData[,] _tilesData;
        
        public void Initialize()
        {
            InitializeSlots();
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
        
        private void InitializeSlots()
        {
            if (Slots == null || Slots.Length != SlotWidth * SlotHeight)
            {
                Slots = new SlotData[SlotWidth * SlotHeight];
            }

            _slotsData = new SlotData[SlotHeight, SlotWidth];

            for (int x = 0; x < SlotHeight; x++)
            {
                for (int y = 0; y < SlotWidth; y++)
                {
                    _slotsData[x, y] = Slots[x * SlotWidth + y];
                }
            }
        }
        
        public TileData GetTile(int x, int y) => _tilesData[x, y];
        
        public SlotData GetSlot(int x, int y) => _slotsData[x, y];
        
    }
}
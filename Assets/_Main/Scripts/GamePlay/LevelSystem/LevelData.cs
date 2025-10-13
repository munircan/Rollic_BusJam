using System;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem;
using Sirenix.OdinInspector;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [Serializable]
    public class LevelData
    {
        [FoldoutGroup("Bus")] public BusData[] Buses;

        [FoldoutGroup("Slot")] public int SlotWidth = 5;
        [FoldoutGroup("Slot")] public int SlotHeight = 1;
        [FoldoutGroup("Slot")] public SlotData[] Slots;

        private SlotData[,] _slotsData;


        [FoldoutGroup("Tile")] public int TileWidth = 5;
        [FoldoutGroup("Tile")] public int TileHeight = 5;
        [FoldoutGroup("Tile")] public TileData[] Tiles;
    }
}
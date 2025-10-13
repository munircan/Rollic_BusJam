using System;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [Serializable]
    public struct LevelData
    {
        [HideInInspector] [FoldoutGroup("Bus")]
        public BusData[] Buses;

        [HideInInspector] [FoldoutGroup("Slot")]
        public int SlotWidth;

        [HideInInspector] [FoldoutGroup("Slot")]
        public int SlotHeight;

        [HideInInspector] [FoldoutGroup("Slot")]
        public SlotData[] Slots;

        [HideInInspector] [FoldoutGroup("Tile")]
        public int TileWidth;

        [HideInInspector] [FoldoutGroup("Tile")]
        public int TileHeight;

        [HideInInspector] [FoldoutGroup("Tile")]
        public TileData[] Tiles;
    }
}
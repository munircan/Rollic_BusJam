using System;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [Serializable]
    public class LevelData
    {
        [HideInInspector] [FoldoutGroup("Bus")]
        public BusData[] Buses;

        [HideInInspector] [FoldoutGroup("Slot")]
        public int SlotWidth = 5;

        [HideInInspector] [FoldoutGroup("Slot")]
        public int SlotHeight = 1;

        [HideInInspector] [FoldoutGroup("Slot")]
        public SlotData[] Slots;

        [HideInInspector] [FoldoutGroup("Tile")]
        public int TileWidth = 5;

        [HideInInspector] [FoldoutGroup("Tile")]
        public int TileHeight = 5;

        [HideInInspector] [FoldoutGroup("Tile")]
        public TileData[] Tiles;
    }
}
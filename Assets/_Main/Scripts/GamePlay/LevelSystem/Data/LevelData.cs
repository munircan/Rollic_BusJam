using System;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem.Data;
using _Main.Scripts.GamePlay.TileSystem.Data;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem.Data
{
    [Serializable]
    public struct LevelData
    {
        [HideInInspector] public int LevelDuration;

        [HideInInspector] public BusData[] Buses;

        [HideInInspector] public int SlotWidth;

        [HideInInspector] public int SlotHeight;

        [HideInInspector] public SlotData[] Slots;

        [HideInInspector] public int TileWidth;

        [HideInInspector] public int TileHeight;

        [HideInInspector] public TileData[] Tiles;
    }
}
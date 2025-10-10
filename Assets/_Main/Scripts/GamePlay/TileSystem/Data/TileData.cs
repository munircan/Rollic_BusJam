using System;
using _Main.Scripts.GamePlay.PersonSystem;

namespace _Main.GamePlay.TileSystem
{
    [Serializable]
    public struct TileData
    {
        public TileType Type;
        public PersonData PersonData;
    }
}
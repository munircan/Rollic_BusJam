using System;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.PersonSystem.Data;
using Sirenix.OdinInspector;

namespace _Main.Scripts.GamePlay.TileSystem.Data
{
    [Serializable]
    public struct TileData
    {
        public TileType Type;
        
        [ShowIf("IsPersonType")] public PersonData PersonData;

        private bool IsPersonType()
        {
            return Type == TileType.Person;
        }
    }
}
using System;
using _Main.Scripts.GamePlay.PersonSystem;
using Sirenix.OdinInspector;

namespace _Main.GamePlay.TileSystem
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
using System;
using _Main.Scripts.GamePlay.PersonSystem;

namespace _Main.Scripts.GamePlay.BusSystem.Data
{
    [Serializable]
    public struct BusData
    {
        public int PersonLimit;
        public PersonColor PersonColor;
    }
}
using System;
using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.GamePlay.Settings;

namespace _Main.Scripts.GamePlay.BusSystem.Data
{
    [Serializable]
    public struct BusData
    {
        public int PersonLimit;
        public ColorType ColorType;
        public Appearance Appearance;
    }
}
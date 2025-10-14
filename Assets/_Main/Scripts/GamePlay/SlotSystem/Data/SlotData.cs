using System;
using _Main.Scripts.GamePlay.Settings;

namespace _Main.Scripts.GamePlay.SlotSystem.Data
{
    [Serializable]
    public struct SlotData
    {
        public bool IsLocked;
        public int LockedLevel;

        public bool IsStillLocked()
        {
            return LevelSettings.Instance.GetCurrentLevel() < LockedLevel;
        }
    }
}
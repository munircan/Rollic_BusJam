using _Main.Scripts.GamePlay.GameStateSystem;
using UnityEngine;

namespace _Main.Scripts.Utilities
{
    public static class GameConfig
    {
        public static GameState State { get; set; }
        
        public static FailReason FailReason { get; set; }
        
        public static int LevelClickCount { get; set; }
        
        public static int CountdownTimer { get; set; }
        
        
        public static class PlayerPref
        {
            public static int CurrentLevel
            {
                get => PlayerPrefs.GetInt(Keys.Prefs.CURRENT_LEVEL,0);
                set => PlayerPrefs.SetInt(Keys.Prefs.CURRENT_LEVEL,value);
            }
        }
    }
}
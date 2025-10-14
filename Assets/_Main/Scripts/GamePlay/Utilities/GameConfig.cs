using _Main.Scripts.GamePlay.GameStateSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Utilities
{
    public static class GameConfig
    {
        public static GameState State { get; set; }
        
        public static FailReason FailReason { get; set; }
        
        public static Camera MainCamera { get; set; }
        
        public static int LevelClickCount { get; set; }
        
        public const float TILE_SIZE = 1.25f;


        public const float CAMERA_PADDING = 5;
        
        
        
        
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
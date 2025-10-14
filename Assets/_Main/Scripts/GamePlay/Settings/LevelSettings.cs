using _Main.Patterns.Singleton;
using _Main.Scripts.GamePlay.Helpers;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.LevelSystem.Data;
using _Main.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/Level Settings", order = 0)]
    public class LevelSettings : SingletonScriptableObject<LevelSettings>
    {
        [SerializeField] private LevelScriptableObject[] _levels;

        [SerializeField] private bool UseSelectedLevel;

        [ShowIf("UseSelectedLevel")] [SerializeField]
        private int CurrentLevel;


        public LevelData GetCurrentLevelData()
        {
            return _levels.GetElementWithMod(GetCurrentLevel()).Data;
        }

        public int GetCurrentLevel()
        {
            if (UseSelectedLevel)
            {
                return CurrentLevel;
            }

            return GameConfig.PlayerPref.CurrentLevel;
        }

        public string GetCurrentLevelName()
        {
            return (GetCurrentLevel() + 1).ToString();
        }

        [Button(ButtonSizes.Gigantic)]
        public void SetPlayerPredCurrentLevel()
        {
            GameConfig.PlayerPref.CurrentLevel = CurrentLevel;
        }
    }
}
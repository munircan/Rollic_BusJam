using _Main.Patterns.Singleton;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/Level Settings", order = 0)]
    public class LevelSettings : SingletonScriptableObject<LevelSettings>
    {
        [SerializeField] private LevelScriptableObject[] _levels;

        public int CurrentLevel;


        public LevelData GetCurrentLevel()
        {
            return _levels.GetElementWithMod(CurrentLevel).Data;
        }
    }
}
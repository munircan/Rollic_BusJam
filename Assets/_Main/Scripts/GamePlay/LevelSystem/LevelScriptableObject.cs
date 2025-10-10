using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/Create Level", order = 0)]
    public class LevelScriptableObject : ScriptableObject
    {
        public LevelData Data;
    }
}
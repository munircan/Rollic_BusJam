using _Main.Patterns.Singleton;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private SingletonScriptableObject[] _settings;
        
        
        public void InitializeManagers()
        {
            var essentialsGameSettingsCount = _settings.Length;
            for (int i = 0; i < essentialsGameSettingsCount; i++)
                _settings[i].Initialize();
            
        }

        public void TerminateManagers()
        {
            var essentialsGameSettingsCount = _settings.Length;
            for (int i = 0; i < essentialsGameSettingsCount; i++)
                _settings[i].Terminate();
        }
        

        private void Awake()
        {
            InitializeManagers();
        }

        private void OnDestroy()
        {
            TerminateManagers();
        }
    }
}
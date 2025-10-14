using _Main.Scripts.Patterns.Singleton;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private SingletonScriptableObject[] _settings;

        #endregion

        #region Unity Events

        private void Awake()
        {
            InitializeManagers();
        }

        private void OnDestroy()
        {
            TerminateManagers();
        }

        #endregion

        #region Init-Terminate

        private void InitializeManagers()
        {
            var essentialsGameSettingsCount = _settings.Length;
            for (int i = 0; i < essentialsGameSettingsCount; i++)
                _settings[i].Initialize();
        }

        private void TerminateManagers()
        {
            var essentialsGameSettingsCount = _settings.Length;
            for (int i = 0; i < essentialsGameSettingsCount; i++)
                _settings[i].Terminate();
        }

        #endregion
    }
}
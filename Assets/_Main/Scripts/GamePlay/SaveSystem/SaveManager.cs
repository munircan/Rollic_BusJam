using System.Collections.Generic;
using System.IO;
using _Main.Scripts.GamePlay.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SaveSystem
{
    public static class SaveManager
    {
        private const string FILE_NAME = "tile_indexes.json";
        
        private static TileIndexData _currentData = new TileIndexData();
        
        public static IReadOnlyList<int> TileIndexes => _currentData.TileIndexes;

        public static void AddTileIndex(int i)
        {
            _currentData.AddTileIndex(i);
            SaveData();
        }

        public static void ClearList()
        {
            _currentData.ClearList();
            GameConfig.IsLoadingFromSave = false;
            SaveData();
        }

        private static void SaveData()
        {
            string path = Path.Combine(Application.persistentDataPath, FILE_NAME);
            
            try
            {
                string json = JsonUtility.ToJson(_currentData, true);
                File.WriteAllText(path, json);
                // Debug.Log($"Data saved to: {path}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to save data: {e.Message}");
            }
        }

        public static void LoadData()
        {
            string path = Path.Combine(Application.persistentDataPath, FILE_NAME);

            if (!File.Exists(path))
            {
                Debug.LogWarning("Save file not found. Creating new data instance.");
                _currentData = new TileIndexData();
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                _currentData = JsonUtility.FromJson<TileIndexData>(json);
                if (_currentData!= null && _currentData.TileIndexes.Count != 0)
                {
                    GameConfig.IsLoadingFromSave = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load data. Resetting to default: {e.Message}");
                _currentData = new TileIndexData();
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents.LevelEvents;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SaveSystem;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.GamePlay.SlotSystem.Manager;
using _Main.Scripts.GamePlay.TileSystem.Manager;
using _Main.Scripts.GamePlay.Utilities;
using _Main.Scripts.Patterns.EventSystem;
using _Main.Scripts.Patterns.ServiceLocation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem.Manager
{
    public class LevelManager : MonoBehaviour
    {
        #region Managers

        private TileManager _tileManager;
        private PersonManager _personManager;
        private SlotManager _slotManager;
        private BusManager _busManager;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _tileManager = ServiceLocator.GetService<TileManager>();
            _personManager = ServiceLocator.GetService<PersonManager>();
            _slotManager = ServiceLocator.GetService<SlotManager>();
            _busManager = ServiceLocator.GetService<BusManager>();

            EventManager.Subscribe<EventLoadLevel>(OnEventLoadLevel);
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<TileManager>();
            ServiceLocator.UnregisterService<PersonManager>();
            ServiceLocator.UnregisterService<SlotManager>();
            ServiceLocator.UnregisterService<BusManager>();

            EventManager.Unsubscribe<EventLoadLevel>(OnEventLoadLevel);
        }

        private void Start()
        {
            LoadLevel();
        }

        #endregion

        #region Load Methods

        private void LoadLevel()
        {
            SaveManager.LoadData();
            GameConfig.LevelClickCount = 0;
            var currentLevelData = LevelSettings.Instance.GetCurrentLevelData();
            _slotManager.CreateSlots(currentLevelData);
            _tileManager.CreateTiles(currentLevelData);
            _personManager.CreatePeople(_tileManager.GetTiles());
            _busManager.CreateBuses(currentLevelData);
            EventManager.Publish(EventLevelLoaded.Create());

            if (!GameConfig.IsLoadingFromSave)
            {
                return;
            }

            StartLevelFromSave().Forget();
        }

        private async UniTask StartLevelFromSave()
        {
            var tiles = _tileManager.GetTiles();
            List<UniTask> tasks = new List<UniTask>();
            foreach (var tileIndex in SaveManager.TileIndexes)
            {
                tasks.Add(tiles[tileIndex].InputController.ExecuteWithObjectManager(false));
            }

            await UniTask.WhenAll(tasks);
            GameConfig.IsLoadingFromSave = false;
        }

        private void RefreshAndLoadLevel()
        {
            UnloadLevel();
            LoadLevel();
        }

        private void UnloadLevel()
        {
            _slotManager.ReleaseSlots();
            _tileManager.ReleaseTiles();
            _personManager.ReleasePeople();
            _busManager.ReleaseBuses();
        }

        #endregion

        #region Succes-Fail

        public static void LevelSuccess()
        {
            SaveManager.ClearList();
            GameConfig.PlayerPref.CurrentLevel++;
            EventManager.Publish(EventLevelSuccess.Create(GameConfig.LevelClickCount));
        }

        public static void LevelFailed()
        {
            SaveManager.ClearList();
            EventManager.Publish(EventLevelFail.Create(GameConfig.LevelClickCount, GameConfig.FailReason));
        }

        public static void LevelRestart()
        {
            SaveManager.ClearList();
        }

        #endregion

        #region Event Methods

        private void OnEventLoadLevel(EventLoadLevel customEvent)
        {
            RefreshAndLoadLevel();
        }

        #endregion
    }
}
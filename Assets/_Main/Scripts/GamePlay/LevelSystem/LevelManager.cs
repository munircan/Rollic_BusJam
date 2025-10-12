using System;
using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private TileManager _tileManager;
        private PersonManager _personManager;
        private SlotManager _slotManager;
        private BusManager _busManager;

        public static GameStateSystem.GameState State { get; private set; }


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


        private void OnEnable()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            GameConfig.LevelClickCount = 0;
            var currentLevelData = LevelSettings.Instance.GetCurrentLevel();
            _slotManager.CreateSlots(currentLevelData);
            _tileManager.CreateTiles(currentLevelData);
            _personManager.CreatePeople(_tileManager.Tiles);
            _busManager.CreateBuses(currentLevelData);
            EventManager.Publish(EventLevelLoad.Create());
        }

        public void RefreshAndLoadLevel()
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


        private void OnEventLoadLevel(EventLoadLevel customEvent)
        {
            RefreshAndLoadLevel();
        }
    }
}
using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.GamePlay.SlotSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private TileManager _tileManager;
        private PersonManager _personManager;
        private SlotManager _slotManager;
        private BusManager _busManager;


        private void Awake()
        {
            _tileManager = ServiceLocator.GetService<TileManager>();
            _personManager = ServiceLocator.GetService<PersonManager>();
            _slotManager = ServiceLocator.GetService<SlotManager>();
            _busManager = ServiceLocator.GetService<BusManager>();
        }

        private void OnEnable()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            var currentLevelData = LevelSettings.Instance.GetCurrentLevel();
            _slotManager.CreateSlots(currentLevelData);
            _tileManager.CreateTiles(currentLevelData);
            _personManager.CreatePeople(_tileManager.Tiles);
            _busManager.CreateBuses(currentLevelData);
        }

        public void UnloadLevel()
        {
            _slotManager.ReleaseSlots();
            _tileManager.ReleaseTiles();
            _personManager.ReleasePeople();
            _busManager.ReleaseBuses();
        }
    }
}
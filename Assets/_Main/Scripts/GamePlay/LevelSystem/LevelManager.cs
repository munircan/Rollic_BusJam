using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SlotSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelScriptableObject  _levelScriptableObject;
        private TileManager  _tileManager;
        private PersonManager   _personManager;
        private SlotManager   _slotManager;
        


        private void Awake()
        {
            _tileManager = ServiceLocator.GetService<TileManager>();
            _personManager = ServiceLocator.GetService<PersonManager>();
            _slotManager = ServiceLocator.GetService<SlotManager>();
        }

        private void OnEnable()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            _slotManager.CreateSlots(_levelScriptableObject.Data);
            _tileManager.CreateTiles(_levelScriptableObject.Data);
            _personManager.CreatePeople(_tileManager.Tiles);
        }

        public void UnloadLevel()
        {
            _slotManager.ReleaseSlots();
            _tileManager.ReleaseTiles();
            _personManager.ReleasePeople();
        }
    }
}
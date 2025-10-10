using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelScriptableObject  _levelScriptableObject;
        private TileManager  _tileManager;
        private PersonManager   _personManager;
        


        private void Awake()
        {
            _tileManager = ServiceLocator.GetService<TileManager>();
            _personManager = ServiceLocator.GetService<PersonManager>();
        }

        private void OnEnable()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            _tileManager.CreateTiles(_levelScriptableObject.Data);
            _personManager.CreatePeople(_tileManager.Tiles);
        }
    }
}
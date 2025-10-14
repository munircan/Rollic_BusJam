using _Main.Scripts.GamePlay.PersonSystem.Data;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.GamePlay.SlotSystem.Components;
using _Main.Scripts.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.TileSystem.Components;
using _Main.Scripts.Patterns.ModuleSystem;
using _Main.Scripts.Patterns.ServiceLocation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Components
{
    public class Person : BaseComponent, ITileObject
    {
        #region SerializeField Variables

        [SerializeField] private PersonModelController _modelController;
        [SerializeField] private PersonMovementController _movementController;

        #endregion
        
        #region Encapsulated Variables

        public Tile Tile { get; set; }

        public Slot Slot { get; set; }

        public PersonData Data { get; private set; }
        
        public PersonMovementController MovementController =>  _movementController;
        
        public PersonModelController ModelController => _modelController;

        #endregion
        
        #region Unity Event Methods

        private void OnDisable()
        {
            Reset();
        }

        #endregion

        #region Init-Reset Methods

        public void Initialize(PersonData data)
        {
            Data = data;
            _modelController.Initialize();
            _movementController.Initialize();
        }

        public void Reset()
        {
            SetTile(null);
            SetSlot(null);
            _modelController.Reset();
            _movementController.Reset();
        }

        #endregion

        #region Set Methods

        public void SetTile(Tile tile)
        {
            Tile = tile;
            if (tile != null)
            {
                Tile.SetTileObject(this, true);
            }
        }

        public void SetSlot(Slot slot)
        {
            Slot = slot;
            if (slot != null)
            {
                Slot.SetPerson(this);
            }
        }
        

        #endregion
        
        #region Implemented Methods

        public void Execute()
        {
            if (ServiceLocator.TryGetService(out PersonManager manager))
            {
                var pathData = manager.GetPersonPathData(this);
                if (pathData.HasPath)
                {
                    Tile.SetTileObject(null);
                    _movementController.MovePathAsync(pathData.PathPositions).Forget();
                }
            }
        }

        #endregion
    }
}
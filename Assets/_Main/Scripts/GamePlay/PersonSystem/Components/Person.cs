using System;
using System.Collections.Generic;
using _Main.GamePlay.TileSystem;
using _Main.Patterns.ModuleSystem;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SlotSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
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

            SetColor();
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

        private void SetColor()
        {
            _modelController.SetColor(Data.Color);
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
                    // Assign the Task result to the discard variable. 
                    // This tells the compiler: "I know this returns a Task, but I'm ignoring it."
                    _ = _movementController.MovePathAsync(pathData.PathPositions);
                }
            }
        }

        #endregion
    }
}
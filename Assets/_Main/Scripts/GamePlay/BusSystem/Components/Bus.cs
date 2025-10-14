using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class Bus : BaseComponent
    {
        #region SerializeFields

        [SerializeField] private BusMovementController _movementController;
        [SerializeField] private BusModelController _modelController;
        [SerializeField] private BusPersonController _personController;

        #endregion

        #region Encapulsation

        public BusMovementController MovementController => _movementController;

        public BusModelController ModelController => _modelController;
        public BusPersonController PersonController => _personController;

        public BusData Data { get; set; }

        #endregion

        #region Init-Reset

        public void Initialize(BusData data)
        {
            Data = data;
            MovementController.Initialize();
            ModelController.Initialize();
            PersonController.Initialize();
        }

        public void Reset()
        {
            MovementController.Reset();
            ModelController.Reset();
            PersonController.Reset();
        }

        #endregion
    }
}
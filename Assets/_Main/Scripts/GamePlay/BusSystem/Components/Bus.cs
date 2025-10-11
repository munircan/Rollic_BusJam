using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class Bus : BaseComponent
    {
        [SerializeField] private BusMovementController _movementController;
        [SerializeField] private BusModelController _modelController;
        [SerializeField] private BusPersonController _personController;

        #region Encapulsation

        public BusMovementController MovementController => _movementController;

        public BusModelController ModelController => _modelController;
        public BusPersonController PersonController => _personController;

        public BusData Data { get; set; }

        #endregion


        public void Initialize(BusData data)
        {
            Data = data;
            MovementController.Initialize();
            ModelController.Initialize();
            PersonController.Initialize();

            PersonController.SetPersonLimit(Data.PersonCount);
            ModelController.SetColor(Data.PersonColor);
        }

        public void Reset()
        {
            MovementController.Reset();
            ModelController.Reset();
            PersonController.Reset();
        }
    }
}
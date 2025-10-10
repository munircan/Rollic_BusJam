using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class Bus : BaseComponent
    {
        [SerializeField] private BusMovementController _movementController;
        [SerializeField] private BusModelController _modelController;
        [SerializeField] private BusPersonController _personController;
        
        private BusData  _busData;
        public void Initialize(BusData data)
        {
            _busData = data;
            _movementController.Initialize();
            _modelController.Initialize();
            _personController.Initialize();
            
            _personController.SetPersonLimit(_busData.PersonCount);
            _modelController.SetColor(_busData.PersonColor);
            
        }

        public void Reset()
        {
            _movementController.Reset();
            _modelController.Reset();
            _personController.Reset();
        }

        public void Move(Vector3 position, MovementType movementType)
        {
            _movementController.Move(position, movementType);
        }
    }
}
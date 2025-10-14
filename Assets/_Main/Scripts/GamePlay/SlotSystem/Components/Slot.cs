using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.GamePlay.SlotSystem.Data;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem.Components
{
    public class Slot : BaseComponent, IGridObject
    {
        #region SerializeFields

        [SerializeField] private SlotModelController  _slotModelController;
        [SerializeField] private Transform _personTransform;

        #endregion

        #region Encapsulation

        public Transform PersonTransform => _personTransform;

        public bool IsOccupied => _person != null;
        public bool IsLocked { get; private set; }

        #endregion

        #region Private Variables

        private SlotData _slotData;

        private Person _person;

        #endregion
        
        #region Init-Reset

        public void Initialize(SlotData data)
        {
            _slotData = data;
            IsLocked = _slotData.IsStillLocked();
            _slotModelController.Initialize();
        }

        public void Reset()
        {
            SetPerson(null);
            _slotModelController.Reset();
        }

        #endregion

        #region Set-Get Methods

        public void SetPerson(Person person)
        {
            _person = person;
        }

        public Person GetPerson()
        {
            return _person;
        }

        #endregion
    }
}
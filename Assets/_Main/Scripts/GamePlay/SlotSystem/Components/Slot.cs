using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.GamePlay.SlotSystem.Data;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem.Components
{
    public class Slot : MonoBehaviour, IGridObject
    {
        #region SerializeFields

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
        }

        public void Reset()
        {
            SetPerson(null);
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
using System;
using _Main.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem
{
    public class Slot : MonoBehaviour, IGridObject
    {
        [SerializeField] private Transform  _personTransform;
        
        
        public Transform  PersonTransform => _personTransform;
        
        
        public bool IsOccupied
        {
            get { return _person != null; }
        }
        public bool IsLocked {get; private set;}

        private SlotData _slotData;

        private Person _person;

        public void Initialize(SlotData data)
        {
            _slotData = data;
            IsLocked = _slotData.IsStillLocked();
        }

        public void SetPerson(Person person)
        {
            _person = person;
        }

        public Person GetPerson()
        {
            return _person;
        }

        public void Reset()
        {
            SetPerson(null);
        }
    }
}
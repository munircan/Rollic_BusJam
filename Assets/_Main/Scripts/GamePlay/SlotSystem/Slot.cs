using System;
using _Main.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem
{
    public class Slot  :MonoBehaviour, IGridObject
    {
        public bool IsOccupied
        {
            get
            {
                return _person != null;
            }
        }

        private SlotData _slotData;

        private Person _person;

        public void Initialize(SlotData data)
        {
            _slotData  = data;
        }

        public void SetPerson(Person person)
        {
            _person = person;
        }

        public void Reset()
        {
            SetPerson(null);
        }
    }
}
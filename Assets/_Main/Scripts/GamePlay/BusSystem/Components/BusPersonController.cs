using System.Collections.Generic;
using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusPersonController : ComponentModule<Bus>
    {
        #region SerializeFields

        // ADD DIFFERENT BUS MODELS AND PARENT LIST LATER
        [SerializeField] private List<Transform> _personParents;

        #endregion

        #region Encapsulation

        public bool IsBusFull => _limit == _personList.Count;

        public bool IsEverybodyIn => _personInBusList.Count == _limit;

        #endregion

        #region Private Variables

        private List<Person> _personList = new();
        private List<Person> _personInBusList = new();
        private int _limit;

        #endregion

        #region Init-Reset

        internal override void Initialize()
        {
            base.Initialize();
            SetPersonLimit();
        }

        internal override void Reset()
        {
            base.Reset();
            _personList.Clear();
            _personInBusList.Clear();
        }

        #endregion
        
        #region Set-Get Methods

        private void SetPersonLimit()
        {
            _limit = BaseComp.Data.PersonLimit;
        }

        public void AddPerson(Person person)
        {
            _personList.Add(person);
        }

        public void AddPersonInBus(Person person)
        {
            _personInBusList.Add(person);
        }

        public Transform GetPersonBusTransform(Person person)
        {
            var personIndex = GetPersonIndex(person);
            return _personParents[personIndex];
        }

        private int GetPersonIndex(Person person)
        {
            return _personList.IndexOf(person);
        }

        public bool IsFullAndEverybodyIn()
        {
            return IsBusFull && IsEverybodyIn;
        }

        #endregion
    }
}
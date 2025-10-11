using System.Collections.Generic;
using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusPersonController : ComponentModule<Bus>
    {
        // ADD DIFFERENT BUS MODELS AND PARENT LIST LATER
        [SerializeField] private List<Transform> _personParents;

        private const float PERSON_OFFSET = 1.25f;
        private List<Person> _people = new();
        private int _limit;

        public bool IsBusFull =>  _limit == _people.Count;
        
    
        public void SetPersonLimit(int limit)
        {
            _limit = limit;
        }
        
        public void AddPerson(Person  person)
        {
            _people.Add(person);
        }

        public Transform GetPersonBusTransform(Person person)
        {
            var personIndex = GetPersonIndex(person);
            return _personParents[personIndex];
        }

        private int GetPersonIndex(Person person)
        {
            return _people.IndexOf(person);
        }

        internal override void Reset()
        {
            base.Reset();
            _people.Clear();
        }
    }
}
using System.Collections.Generic;
using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusPersonController : ComponentModule<Bus>
    {
        [SerializeField] private Transform _personParent;

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
            //TODO: PERSON MOVE AND JUMP
        }

        private Vector3 GetNextPersonLocalPosition(int index)
        {
            var startPos = _personParent.position;
            var pos = new Vector3(startPos.x + (index) * PERSON_OFFSET, startPos.y, startPos.z);
            return pos;
        }

        internal override void Reset()
        {
            base.Reset();
            _people.Clear();
        }
    }
}
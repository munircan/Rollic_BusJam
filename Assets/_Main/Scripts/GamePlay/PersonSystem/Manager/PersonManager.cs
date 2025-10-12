using System.Collections.Generic;
using System.Linq;
using _Main.GamePlay.TileSystem;
using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Manager
{
    public class PersonManager : MonoBehaviour
    {
        [SerializeField] private Transform _personParent;
        private List<Person> _personList;
        private Dictionary<Person, PersonPathData> _personPathDictionary;
        
        private Dictionary<Bus,List<Person>> _slotToBusPeopleDictionary;

        private void Awake()
        {
            EventManager.Subscribe<EventTileChanged>(OnEventTileChanged);
            EventManager.Subscribe<EventBusChanged>(OnEventBusChanged);
            EventManager.Subscribe<EventBusMovedIn>(OnEventBusMovedIn);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventTileChanged>(OnEventTileChanged);
            EventManager.Unsubscribe<EventBusChanged>(OnEventBusChanged);
            EventManager.Unsubscribe<EventBusMovedIn>(OnEventBusMovedIn);
        }

        public void CreatePeople(List<Tile> tiles)
        {
            _personList = new List<Person>();
            
            _slotToBusPeopleDictionary = new Dictionary<Bus, List<Person>>();
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                if (tile.Data.Type == TileType.Person)
                {
                    var personPosition = tile.GetPersonPosition();
                    var personData = tile.Data.PersonData;
                    var person = ObjectPooler.Instance.SpawnSc<Person>(Keys.PERSON_POOL_TAG, personPosition,
                        Quaternion.identity, _personParent);
                    person.Initialize(personData);
                    person.SetTile(tile);
                    _personList.Add(person);
                }
            }

            SetPeopleCanWalk();
        }

        public void ReleasePeople()
        {
            foreach (var person in _personList)
            {
                person.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.PERSON_POOL_TAG, person);
            }
            _personList.Clear();
            _slotToBusPeopleDictionary.Clear();
        }

        private void SetPeopleCanWalk()
        {
            _personPathDictionary = new Dictionary<Person, PersonPathData>();
            if (ServiceLocator.TryGetService(out TileManager tileManager))
            {
                for (var i = 0; i < _personList.Count; i++)
                {
                    var person = _personList[i];
                    var personTile = person.Tile;
                    if (personTile == null)
                    {
                        continue;
                    }

                    var hasPath = tileManager.GetPath(personTile, out var path);
                    var personPathData = new PersonPathData();
                    personPathData.HasPath = hasPath;
                    personPathData.PathPositions = path;
                    _personPathDictionary.Add(person, personPathData);
                }
            }
        }

        private void OnEventTileChanged(EventTileChanged customEvent)
        {
            SetPeopleCanWalk();
        }

        private void OnEventBusChanged(EventBusChanged eventBusChanged)
        {
            SetSlotToBusPeople(eventBusChanged.Bus);
        }

        private void OnEventBusMovedIn(EventBusMovedIn eventBusMovedIn)
        {
            SlotToBusPeopleMovement(eventBusMovedIn.Bus);
        }

        private void SlotToBusPeopleMovement(Bus bus)
        {
            if (_slotToBusPeopleDictionary.TryGetValue(bus, out var personList))
            {
                for (var i = 0; i < personList.Count; i++)
                {
                    var person = personList[i];
                    person.MovementController.MoveToBus(bus);
                }
            }
        }

        private void SetSlotToBusPeople(Bus bus)
        {
            var slotPersonList = GetSlotPersonList();
            var personList = new List<Person>();

            for (var i = 0; i < slotPersonList.Count; i++)
            {
                var person = slotPersonList[i];
                if (bus.PersonController.IsBusFull)
                {
                    break;
                }

                if (person.Data.Color == bus.Data.PersonColor)
                {
                    person.Slot.SetPerson(null);
                    person.SetSlot(null);
                    bus.PersonController.AddPerson(person);
                    personList.Add(person);
                }
            }
            _slotToBusPeopleDictionary.Add(bus, personList);
        }


        private List<Person> GetSlotPersonList()
        {
            var slotManager = ServiceLocator.GetService<SlotManager>();
            return slotManager.GetSlotPersonList().ToList();
        }


        public PersonPathData GetPersonPathData(Person person)
        {
            return _personPathDictionary[person];
        }
    }

    public struct PersonPathData
    {
        public bool HasPath;
        public List<Vector3> PathPositions;
    }
}
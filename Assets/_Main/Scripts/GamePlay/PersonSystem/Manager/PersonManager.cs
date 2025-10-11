using System;
using System.Collections.Generic;
using _Main.GamePlay.TileSystem;
using _Main.GamePlay.TileSystem.Manager;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Manager
{
    public class PersonManager : MonoBehaviour
    {
        [SerializeField] private Transform _personParent;
        private List<Person> _personList;
        private Dictionary<Person, PersonPathData> _personPathDictionary;

        private void Awake()
        {
            EventManager.Subscribe<EventTileChanged>(OnEventTileChanged);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventTileChanged>(OnEventTileChanged);
        }

        public void CreatePeople(List<Tile> tiles)
        {
            _personList = new List<Person>();
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
        }

        private void SetPeopleCanWalk()
        {
            _personPathDictionary = new();
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
                    if (hasPath)
                    {
                        personPathData.PathPositions = new List<Vector3>(path.GetPathAsVector3List());
                    }
                    
                    _personPathDictionary.Add(person, personPathData);
                }
            }
        }

        private void OnEventTileChanged(EventTileChanged customEvent)
        {
            SetPeopleCanWalk();
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
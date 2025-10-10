using System.Collections.Generic;
using _Main.GamePlay.TileSystem;
using _Main.Patterns.ObjectPooling;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Manager
{
    public class PersonManager : MonoBehaviour
    {
        [SerializeField] private Transform _personParent;
        private List<Person> _personList;
       
        private const string POOL_TAG = "Person";

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
                    var person = ObjectPooler.Instance.SpawnSc<Person>(POOL_TAG,personPosition,Quaternion.identity,_personParent);
                    person.Initialize(personData);
                    person.SetTile(tile);
                    _personList.Add(person);
                }
            }
        }

    }
}
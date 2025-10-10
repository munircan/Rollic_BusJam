using System.Collections.Generic;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.LevelSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Manager
{
    public class BusManager : MonoBehaviour
    {
        [SerializeField] private Transform _busParent;
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Transform _endTransform;
        private const string POOL_TAG = "Bus";
        private const float BUS_OFFSET = 5;
        private List<Bus> _buses;
        private int _currentIndex;
     
        
        public void CreateBuses(LevelData  levelData)
        {
            _buses = new List<Bus>();
            var startPos = _busParent.position;
            var busesData = levelData.Buses;
            for (var i = 0; i < busesData.Length; i++)
            {
                var data = busesData[i];
                var pos = new Vector3(startPos.x - (i * BUS_OFFSET) , startPos.y,startPos.z);
                var bus = ObjectPooler.Instance.SpawnSc<Bus>(POOL_TAG, pos, Quaternion.identity, _busParent);
                bus.Initialize(data);
            }
        }

        public void ReleaseBuses()
        {
            _currentIndex = 0;
            foreach (var bus in _buses)
            {
                bus.Reset();
                ObjectPooler.Instance.ReleasePooledObject(POOL_TAG,bus);
            }
        }
    }
}
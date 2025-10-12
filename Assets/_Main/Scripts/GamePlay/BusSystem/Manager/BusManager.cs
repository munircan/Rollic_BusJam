using System;
using System.Collections.Generic;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Manager
{
    public class BusManager : MonoBehaviour
    {
        [SerializeField] private Transform _busParent;
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Transform _endTransform;
        private const float BUS_OFFSET = 5;
        private List<Bus> _buses;
        private int _currentIndex;

        public bool IsBusesFinished => _currentIndex >= _buses.Count;

        private void Awake()
        {
            EventManager.Subscribe<EventPersonGetIntoBus>(OnPersonGetIntoBus);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPersonGetIntoBus>(OnPersonGetIntoBus);
        }


        public void CreateBuses(LevelData levelData)
        {
            _buses = new List<Bus>();
            var startPos = _busParent.position;
            var busesData = levelData.Buses;
            for (var i = 0; i < busesData.Length; i++)
            {
                var data = busesData[i];
                var pos = new Vector3(startPos.x - (i * BUS_OFFSET), startPos.y, startPos.z);
                var bus = ObjectPooler.Instance.SpawnSc<Bus>(Keys.PoolTags.BUS, pos, Quaternion.identity, _busParent);
                bus.Initialize(data);
                _buses.Add(bus);
            }
        }

        public void ReleaseBuses()
        {
            for (var i = 0; i < _buses.Count; i++)
            {
                var bus = _buses[i];
                bus.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.PoolTags.BUS, bus);
            }

            _currentIndex = 0;
            _buses.Clear();
        }

        private void OnPersonGetIntoBus(EventPersonGetIntoBus customEvent)
        {
            var currentBus = GetCurrentBus();
            currentBus.PersonController.AddPersonInBus(customEvent.Person);
            var isLastPerson = currentBus.PersonController.IsFullAndEverybodyIn();
            if (isLastPerson)
            {
                _currentIndex++;
                currentBus.MovementController.SetOnMovementCompleteEvent(SetNextBus);
                currentBus.MovementController.Move(_endTransform.position, MovementType.Out);
                if (!IsBusesFinished)
                {
                    EventManager.Publish(EventBusChanged.Create(GetCurrentBus()));
                }
            }
        }

        private void SetNextBus()
        {
            if (IsBusesFinished)
            {
                GameConfig.PlayerPref.CurrentLevel++;
                EventManager.Publish(EventLevelSuccess.Create(GameConfig.LevelClickCount));
                return;
            }

            var currentBus = GetCurrentBus();
            currentBus.MovementController.SetOnMovementCompleteEvent(OnBusMovedIn);
            currentBus.MovementController.Move(_startTransform.position, MovementType.In);
        }

        private void OnBusMovedIn()
        {
            EventManager.Publish(EventBusMovedIn.Create(GetCurrentBus()));
        }

        public Bus GetCurrentBus()
        {
            return _buses[_currentIndex];
        }
    }
}
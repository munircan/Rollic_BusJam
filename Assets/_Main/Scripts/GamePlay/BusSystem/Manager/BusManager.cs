using System.Collections.Generic;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Manager
{
    public class BusManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform _busParent;
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Transform _endTransform;

        #endregion

        #region Private Variables

        private const float BUS_OFFSET = 5;
        private List<Bus> _buses;
        private int _currentIndex;

        #endregion

        #region Encapsulation

        public bool IsBusesFinished => _currentIndex >= _buses.Count;

        #endregion

        #region Unity Events

        private void Awake()
        {
            EventManager.Subscribe<EventPersonGetIntoBus>(OnPersonGetIntoBus);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPersonGetIntoBus>(OnPersonGetIntoBus);
        }

        #endregion

        #region Create-Release

        public void CreateBuses(LevelData levelData)
        {
            _buses = new List<Bus>();
            var startPos = _busParent.position;
            var busesData = levelData.Buses;
            for (var i = 0; i < busesData.Length; i++)
            {
                var data = busesData[i];
                var pos = new Vector3(startPos.x - ((i + 1) * BUS_OFFSET), startPos.y, startPos.z);
                var bus = ObjectPooler.Instance.SpawnSc<Bus>(Keys.PoolTags.BUS, pos, Quaternion.identity, _busParent);
                bus.Initialize(data);
                _buses.Add(bus);
            }

            MoveCurrentBus().Forget();
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

        #endregion

        #region Event Methods

        private void OnPersonGetIntoBus(EventPersonGetIntoBus customEvent)
        {
            MoveBusIfFull(customEvent).Forget();
        }

        private async UniTask MoveBusIfFull(EventPersonGetIntoBus customEvent)
        {
            var currentBus = GetCurrentBus();
            currentBus.PersonController.AddPersonInBus(customEvent.Person);
            var isLastPerson = currentBus.PersonController.IsFullAndEverybodyIn();
            if (isLastPerson)
            {
                _currentIndex++;
                if (!IsBusesFinished)
                {
                    EventManager.Publish(EventBusChanged.Create(GetCurrentBus()));
                }

                await currentBus.MovementController.Move(_endTransform.position, MovementType.Out);
                await MoveCurrentBus();
            }
        }

        #endregion

        #region Set-Get Methods

        public Bus GetCurrentBus()
        {
            if (_currentIndex >= _buses.Count)
            {
                return null;
            }

            return _buses[_currentIndex];
        }

        #endregion

        #region Move Methods

        private async UniTask MoveCurrentBus()
        {
            if (IsBusesFinished)
            {
                LevelManager.LevelSuccess();
                return;
            }

            var currentBus = GetCurrentBus();
            await currentBus.MovementController.Move(_startTransform.position, MovementType.In);
            EventManager.Publish(EventBusMovedIn.Create(GetCurrentBus()));
            MoveNextBuses().Forget();
        }

        private async UniTask MoveNextBuses()
        {
            int count = 0;
            for (int i = _currentIndex + 1; i < _buses.Count; i++)
            {
                var nextBus = _buses[i];
                var xPos = _startTransform.position.x + (count + 1) * -BUS_OFFSET;
                var newPos = new Vector3(xPos, _startTransform.position.y, _startTransform.position.z);
                await nextBus.MovementController.Move(newPos, MovementType.In);
                count++;
            }
        }

        #endregion
    }
}
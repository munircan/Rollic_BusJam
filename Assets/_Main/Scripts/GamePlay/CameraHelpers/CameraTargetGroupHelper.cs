using System.Linq;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents.LevelEvents;
using _Main.Scripts.GamePlay.SlotSystem.Manager;
using _Main.Scripts.GamePlay.TileSystem.Manager;
using _Main.Scripts.Patterns.EventSystem;
using _Main.Scripts.Patterns.ServiceLocation;
using UnityEngine;

namespace _Main.Scripts.GamePlay.CameraHelpers
{
    public class CameraTargetGroupHelper : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform _busTransform;
        [SerializeField] private Transform _firstSlotTransform;
        [SerializeField] private Transform _lastSlotTransform;
        [SerializeField] private Transform _firsttTileTrasnform;
        [SerializeField] private Transform _lastTileTrasnform;

        #endregion

        #region Unity Events

        private void Awake()
        {
            EventManager.Subscribe<EventLevelLoaded>(OnLevelLoaded);
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventLevelLoaded>(OnLevelLoaded);
        }

        #endregion

        #region Set Methods

        private void SetTilesPosition(Vector3 first, Vector3 last)
        {
            _firsttTileTrasnform.position = first;
            _lastTileTrasnform.position = last;
        }

        private void SetSlotsPosition(Vector3 first, Vector3 last)
        {
            _firstSlotTransform.position = first;
            _lastSlotTransform.position = last;
        }

        private void SetBusTransform(Vector3 pos)
        {
            _busTransform.position = pos;
        }

        #endregion

        #region Event Method

        private void OnLevelLoaded(EventLevelLoaded customEvent)
        {
            var busManager = ServiceLocator.GetService<BusManager>();
            var slotManager = ServiceLocator.GetService<SlotManager>();
            var tileManager = ServiceLocator.GetService<TileManager>();

            SetBusTransform(busManager.transform.position);

            var slots = slotManager.GetSlots();
            var firstSlot = slots.First();
            var lastSlot = slots.Last();
            SetSlotsPosition(firstSlot.transform.position, lastSlot.transform.position);

            var tiles = tileManager.GetTiles();
            var firstTile = tiles.First();
            var lastTile = tiles.Last();
            SetTilesPosition(firstTile.transform.position, lastTile.transform.position);
        }

        #endregion
    }
}
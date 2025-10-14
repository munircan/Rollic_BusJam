using System.Collections.Generic;
using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.LevelSystem.Data;
using _Main.Scripts.GamePlay.LevelSystem.Helpers;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.GamePlay.SlotSystem.Components;
using _Main.Scripts.GamePlay.Utilities;
using _Main.Scripts.Patterns.ObjectPooling;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem.Manager
{
    public class SlotManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform _gridParent;

        #endregion

        #region Private Variables

        private List<Slot> _slots;
        private Grid<Slot> _grid;
        private const float CELL_SIZE = 1.25f;

        #endregion

        #region Create-Release

        public void CreateSlots(LevelData levelData)
        {
            _slots = new List<Slot>();
            var width = levelData.SlotWidth;
            var height = levelData.SlotHeight;
            _grid = new Grid<Slot>(width, height, CELL_SIZE, _gridParent.position);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var tileData = levelData.GetSlot(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Slot>(Keys.PoolTags.SLOT, pos, Quaternion.identity,
                        _gridParent);
                    tile.Initialize(tileData);
                    _slots.Add(tile);
                }
            }
        }

        public void ReleaseSlots()
        {
            foreach (var slot in _slots)
            {
                slot.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.PoolTags.SLOT, slot);
            }

            _slots.Clear();
        }

        #endregion

        #region Get Methods

        public Slot GetFirstEmptySlot()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                if (slot.IsLocked || slot.IsOccupied)
                {
                    continue;
                }

                return slot;
            }

            return null;
        }

        public IEnumerable<Person> GetSlotPersonList()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                if (slot.IsOccupied)
                {
                    yield return slot.GetPerson();
                }
            }
        }

        #endregion
    }
}
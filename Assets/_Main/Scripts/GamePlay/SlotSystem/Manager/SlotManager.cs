using System.Collections.Generic;
using _Main.GamePlay.GridSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        
        public List<Slot> Slots { get; private set; }


        private Grid<Slot> _grid;
        private const float CELL_SIZE = 1.25f;
        
        public void CreateSlots(LevelData levelData)
        {
            Slots = new List<Slot>();
            levelData.Initialize();
            var width = levelData.SlotWidth;
            var height = levelData.SlotHeight;
            _grid = new Grid<Slot>(width, height, CELL_SIZE,_gridParent.position);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var tileData = levelData.GetSlot(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Slot>(Keys.SLOT_POOL_TAG, pos, Quaternion.identity, _gridParent);
                    tile.Initialize(tileData);
                    Slots.Add(tile);
                }
            }
        }

        public void ReleaseSlots()
        {
            foreach (var slot in Slots)
            {
                slot.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.SLOT_POOL_TAG, slot);
            }
            Slots.Clear();
        }

        public Slot GetFirstEmptySlot()
        {
            for (var i = 0; i < Slots.Count; i++)
            {
                var slot = Slots[i];
                if (slot.IsLocked || slot.IsOccupied)
                {
                    continue;
                }

                return slot;
            }

            Debug.Log("There is no empty slot");
            return null;
        }

        public IEnumerable<Person> GetSlotPersonList()
        {
            for (var i = 0; i < Slots.Count; i++)
            {
                var slot = Slots[i];
                if (slot.IsOccupied)
                {
                    yield return slot.GetPerson();
                }
            }
        }
    }
}
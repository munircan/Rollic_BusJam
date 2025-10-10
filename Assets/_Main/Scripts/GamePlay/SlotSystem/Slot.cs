using System;
using _Main.GamePlay.GridSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem
{
    public class Slot  :MonoBehaviour, IGridObject
    {
        public bool IsOccupied { get; set; }
        
        private SlotData _slotData;

        public void Initialize(SlotData data)
        {
            _slotData  = data;
        }

        public void Reset()
        {
            
        }
    }
}
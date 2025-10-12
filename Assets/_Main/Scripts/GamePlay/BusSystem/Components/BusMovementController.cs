using System;
using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusMovementController : ComponentModule<Bus>
    {
        [SerializeField] private BusMovementData _busMovementData;

        private Action _onMovementComplete;

        public void SetOnMovementCompleteEvent(Action action)
        {
            _onMovementComplete = action;
        }
        
        public void Move(Vector3 position, MovementType movementType)
        {
            var movementData = _busMovementData.GetMovementData(movementType);
            transform.DOMove(position, movementData.Duration).SetEase(movementData.Ease).SetLink(gameObject).OnComplete(OnMovementComplete);
          
        }

        private void OnMovementComplete()
        {
            _onMovementComplete?.Invoke();
            SetOnMovementCompleteEvent(null);
        }


        internal override void Reset()
        {
            base.Reset();
            transform.DOKill();
        }
    }
}
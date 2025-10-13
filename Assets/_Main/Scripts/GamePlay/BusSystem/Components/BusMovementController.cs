using System;
using System.Threading.Tasks;
using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusMovementController : ComponentModule<Bus>
    {
        [SerializeField] private BusMovementData _busMovementData;
        
        
        public async Task Move(Vector3 position, MovementType movementType)
        {
            var movementData = _busMovementData.GetMovementData(movementType);
            var moveTween = transform.DOMove(position, movementData.Duration).SetEase(movementData.Ease).SetLink(gameObject);
            await moveTween.AsyncWaitForCompletion();
        }
        

        internal override void Reset()
        {
            base.Reset();
            transform.DOKill();
        }
    }
}
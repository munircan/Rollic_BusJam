using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusMovementController : ComponentModule<Bus>
    {
        [SerializeField] private BusMovementData _busMovementData;


        public void Move(Vector3 position, MovementType movementType)
        {
            var movementData = _busMovementData.GetMovementData(movementType);
            transform.DOMove(position, movementData.Duration).SetEase(movementData.Ease).SetLink(gameObject);
        }

        internal override void Reset()
        {
            base.Reset();
        }
    }
}
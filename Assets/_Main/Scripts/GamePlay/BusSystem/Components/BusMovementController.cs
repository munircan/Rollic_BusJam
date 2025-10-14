using _Main.Scripts.GamePlay.BusSystem.Data;
using _Main.Scripts.Patterns.ModuleSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusMovementController : ComponentModule<Bus>
    {
        #region MyRegion

        [SerializeField] private BusMovementData _busMovementData;

        #endregion

        #region Movement Methods

        public async UniTask Move(Vector3 position, MovementType movementType)
        {
            var movementData = _busMovementData.GetMovementData(movementType);
            var moveTween = transform.DOMove(position, movementData.Duration).SetEase(movementData.Ease)
                .SetLink(gameObject);
            await moveTween.AsyncWaitForCompletion();
        }

        #endregion
        
        #region Init-Reset

        public override void Reset()
        {
            base.Reset();
            transform.DOKill();
        }

        #endregion
    }
}
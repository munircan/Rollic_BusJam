using System.Collections.Generic;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents.InGameEvents;
using _Main.Scripts.GamePlay.LevelSystem.Manager;
using _Main.Scripts.GamePlay.PersonSystem.Data;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.GamePlay.SlotSystem.Components;
using _Main.Scripts.GamePlay.SlotSystem.Manager;
using _Main.Scripts.Patterns.EventSystem;
using _Main.Scripts.Patterns.ModuleSystem;
using _Main.Scripts.Patterns.ServiceLocation;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Components
{
    public class PersonMovementController : ComponentModule<Person>
    {
        #region SerializeFields

        [SerializeField] private PersonMovementData _movementData;

        #endregion

        #region Private Variables

        private Tween _pathTween;

        #endregion

        #region Movement Methods

        public async UniTask MovePathAsync(List<Vector3> path)
        {
            var pathArray = path.ToArray();

            _pathTween = transform.DOPath(pathArray, _movementData.PathMovementData.Duration,
                    _movementData.PathMovementData.PathType, _movementData.PathMovementData.PathMode)
                .SetEase(_movementData.PathMovementData.Ease)
                .SetLink(gameObject)
                .SetSpeedBased(true);
            // .SetLookAt(0.01f);

            await _pathTween.AsyncWaitForCompletion();

            OnPathMovementComplete();
        }

        private void OnPathMovementComplete()
        {
            // CHANGE THIS LOGIC AFTER TRY TO MOVE PERSON MANAGER
            var currentBus = ServiceLocator.GetService<BusManager>().GetCurrentBus();

            var firstEmptySlot = ServiceLocator.GetService<SlotManager>().GetFirstEmptySlot();
            if (currentBus && currentBus.Data.ColorType == BaseComp.Data.colorType &&
                !currentBus.PersonController.IsBusFull)
            {
                currentBus.PersonController.AddPerson(BaseComp);
                MoveToBusAsync(currentBus).Forget();
            }
            else if (firstEmptySlot)
            {
                BaseComp.SetSlot(firstEmptySlot);
                MoveToSlot(firstEmptySlot).Forget();
            }
            else
            {
                LevelManager.LevelFailed();
            }
        }

        private async UniTask MoveToSlot(Slot slot)
        {
            var slotPersonTransform = slot.PersonTransform;
            var slotTween = transform.DOMove(slotPersonTransform.position, _movementData.SlotMovementData.Duration)
                .SetEase(_movementData.SlotMovementData.Ease).SetLink(gameObject);
            await slotTween.AsyncWaitForCompletion();
            OnPersonMoveToSlot();
        }

        public async UniTask MoveToBusAsync(Bus bus)
        {
            var busTransform = bus.PersonController.GetPersonBusTransform(BaseComp);
            transform.SetParent(busTransform);
            var jumpTween = transform.DOLocalJump(Vector3.zero, _movementData.BusMovementData.JumpPower,
                    _movementData.BusMovementData.JumpCount, _movementData.BusMovementData.Duration)
                .SetEase(_movementData.BusMovementData.Ease).SetLink(gameObject);

            await jumpTween.AsyncWaitForCompletion();
            OnPersonMoveToBus();
        }

        #endregion

        #region Movement Complete Methods

        private void OnPersonMoveToBus()
        {
            EventManager.Publish(EventPersonGetIntoBus.Create(BaseComp));
        }

        private void OnPersonMoveToSlot()
        {
        }

        #endregion
        
        #region Init-Reset

        public override void Reset()
        {
            base.Reset();
            transform.DOKill();
            _pathTween.Kill();
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ModuleSystem;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.CustomEvents.InGameEvents;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonMovementController : ComponentModule<Person>
    {
        [SerializeField] private PersonMovementData _movementData;
        private Tween _pathTween;

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

        public void OnPathMovementComplete()
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

        public async UniTask MoveToSlot(Slot slot)
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

        private void OnPersonMoveToBus()
        {
            EventManager.Publish(EventPersonGetIntoBus.Create(BaseComp));
        }

        private void OnPersonMoveToSlot()
        {
        }

        public void KillPath()
        {
            _pathTween.Kill();
        }


        internal override void Reset()
        {
            base.Reset();
            transform.DOKill();
            KillPath();
        }
    }
}
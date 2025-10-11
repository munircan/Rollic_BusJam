using System.Collections.Generic;
using _Main.Patterns.EventSystem;
using _Main.Patterns.ModuleSystem;
using _Main.Patterns.ServiceLocation;
using _Main.Scripts.GamePlay.BusSystem.Components;
using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.SlotSystem;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonMovementController : ComponentModule<Person>
    {
        [SerializeField] private PersonMovementData _movementData;
        private Tween pathTween;

        public void MovePath(List<Vector3> path)
        {
            var pathArray = path.ToArray();
            pathTween = transform.DOPath(pathArray, _movementData.PathMovementData.Duration,
                    _movementData.PathMovementData.PathType, _movementData.PathMovementData.PathMode)
                .SetEase(_movementData.PathMovementData.Ease)
                .SetLink(gameObject)
                .OnComplete(OnPathMovementComplete);
            // .SetLookAt(0.01f);
        }

        public void OnPathMovementComplete()
        {
            // CHANGE THIS LOGIC AFTER TRY TO MOVE PERSON MANAGER
            var currentBus = ServiceLocator.GetService<BusManager>().GetCurrentBus();
            var firstEmptySlot = ServiceLocator.GetService<SlotManager>().GetFirstEmptySlot();
            if (currentBus.Data.PersonColor == BaseComp.Data.Color && !currentBus.PersonController.IsBusFull)
            {
                currentBus.PersonController.AddPerson(BaseComp);
                MoveToBus(currentBus);
            }
            else if (firstEmptySlot != null)
            {
                firstEmptySlot.SetPerson(BaseComp);
                MoveToSlot(firstEmptySlot);
            }
            else
            {
                Debug.Log("Game is over!");
            }
        }

        public void MoveToSlot(Slot slot)
        {
            var slotPersonTransform = slot.PersonTransform;
            transform.DOMove(slotPersonTransform.position, _movementData.SlotMovementData.Duration)
                .SetEase(_movementData.SlotMovementData.Ease).SetLink(gameObject);
        }

        public void MoveToBus(Bus bus)
        {
            var busTransform = bus.PersonController.GetPersonBusTransform(BaseComp);
            transform.SetParent(busTransform);
            transform.DOLocalJump(Vector3.zero, _movementData.BusMovementData.JumpPower,
                    _movementData.BusMovementData.JumpCount, _movementData.BusMovementData.Duration)
                .SetEase(_movementData.BusMovementData.Ease).SetLink(gameObject).OnComplete(OnPersonMoveToBus);
        }

        private void OnPersonMoveToBus()
        {
            EventManager.Publish(EventPersonGetIntoBus.Create(BaseComp));
        }
        
        public void KillPath()
        {
            pathTween.Kill();
        }


        internal override void Reset()
        {
            base.Reset();
            KillPath();
        }
    }
}
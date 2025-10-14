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

        public async UniTask MovePathAsync(List<Vector3> path,bool isInstant)
        {
            var pathArray = path.ToArray();

            if (!isInstant)
            {
                _pathTween = transform.DOPath(pathArray, _movementData.PathMovementData.Duration,
                        _movementData.PathMovementData.PathType, _movementData.PathMovementData.PathMode)
                    .SetEase(_movementData.PathMovementData.Ease)
                    .SetLink(gameObject)
                    .SetSpeedBased(true);
                // .SetLookAt(0.01f);

                await _pathTween.AsyncWaitForCompletion();
            }
        }
        

        public async UniTask MoveToSlot(Slot slot,bool isInstant)
        {
            var slotPersonTransform = slot.PersonTransform;
            if (isInstant)
            {
                transform.position = slotPersonTransform.position;
            }
            else
            {
                var slotTween = transform.DOMove(slotPersonTransform.position, _movementData.SlotMovementData.Duration)
                    .SetEase(_movementData.SlotMovementData.Ease).SetLink(gameObject);
                await slotTween.AsyncWaitForCompletion();
            }
         
        }

        public async UniTask MoveToBusAsync(Bus bus,bool instant)
        {
            var busTransform = bus.PersonController.GetPersonBusTransform(BaseComp);
            transform.SetParent(busTransform);
            if (instant)
            {
                transform.localPosition = Vector3.zero;
            }
            else
            {
                var jumpTween = transform.DOLocalJump(Vector3.zero, _movementData.BusMovementData.JumpPower,
                        _movementData.BusMovementData.JumpCount, _movementData.BusMovementData.Duration)
                    .SetEase(_movementData.BusMovementData.Ease).SetLink(gameObject);

                await jumpTween.AsyncWaitForCompletion();
            }
          
            EventManager.Publish(EventPersonGetIntoBus.Create(BaseComp,instant));
            
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
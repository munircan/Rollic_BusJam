using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.SlotSystem.Components
{
    public class SlotModelController : ComponentModule<Slot>
    {
        #region SerializeFields

        [SerializeField] private GameObject _slotLockModel;

        #endregion
        
        #region Init-Reset

        public override void Initialize()
        {
            base.Initialize();
            OpenSlotLockModelIfLocked();
        }

        public override void Reset()
        {
            base.Reset();
            _slotLockModel.SetActive(false);
        }

        #endregion

        #region Model Methods

        private void OpenSlotLockModelIfLocked()
        {
            if (BaseComp.IsLocked)
            {
                _slotLockModel.SetActive(true);
            }
        }

        #endregion
    }
}
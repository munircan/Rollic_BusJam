using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Components
{
    public class PersonModelController : ComponentModule<Person>
    {
        #region SerializeFields

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Outline _outline;

        #endregion

        #region Init-Reset

        internal override void Initialize()
        {
            base.Initialize();
            SetMaterial();
        }

        internal override void Reset()
        {
            base.Reset();
            SetOutlineEnable(false);
        }

        #endregion

        #region Set-Get

        private void SetMaterial()
        {
            _meshRenderer.material =
                ColorSettings.Instance.GetPersonMaterial(BaseComp.Data.colorType, BaseComp.Data.Appearance, false);
        }


        public void SetOutlineEnable(bool value)
        {
            _outline.enabled = value;
        }

        #endregion
    }
}
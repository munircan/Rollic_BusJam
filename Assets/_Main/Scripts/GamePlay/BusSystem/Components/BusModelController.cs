using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusModelController : ComponentModule<Bus>
    {
        #region SerializeFields

        [SerializeField] private MeshRenderer _meshRenderer;

        #endregion

        #region Init-Reset

        internal override void Initialize()
        {
            base.Initialize();
            SetColor();
        }

        private void SetColor()
        {
            _meshRenderer.material = ColorSettings.Instance.GetMaterial(BaseComp.Data.ColorType);
        }

        #endregion
    }
}
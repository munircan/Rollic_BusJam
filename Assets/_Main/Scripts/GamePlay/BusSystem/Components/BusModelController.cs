using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusModelController : ComponentModule<Bus>
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        internal override void Initialize()
        {
            base.Initialize();
            SetColor();
        }

        public void SetColor()
        {
            _meshRenderer.material= ColorSettings.Instance.GetMaterial(BaseComp.Data.PersonColor);
        }
        

        internal override void Reset()
        {
            base.Reset();
        }
    }
}
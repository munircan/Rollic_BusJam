using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusModelController : ComponentModule<Bus>
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        
        public void SetColor(PersonColor color)
        {
            _meshRenderer.material= ColorSettings.Instance.GetMaterial(color);
        }
        

        internal override void Reset()
        {
            base.Reset();
        }
    }
}
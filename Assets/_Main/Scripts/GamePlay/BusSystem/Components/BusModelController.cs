using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Components
{
    public class BusModelController : ComponentModule<Bus>
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        
        public void SetColor(PersonColor color)
        {
            _meshRenderer.material.color = GetColor(color);
        }


        private Color GetColor(PersonColor color)
        {
            return color switch
            {
                PersonColor.White => Color.white,
                PersonColor.Blue => Color.blue,
                PersonColor.Red => Color.red,
                PersonColor.Green => Color.green,
            };
        }

        internal override void Reset()
        {
            base.Reset();
        }
    }
}
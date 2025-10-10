using _Main.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonModelController : ComponentModule<Person>
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
    }
}
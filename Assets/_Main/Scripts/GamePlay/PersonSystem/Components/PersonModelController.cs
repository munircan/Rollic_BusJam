using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonModelController : ComponentModule<Person>
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void SetColor(PersonColor color)
        {
            _meshRenderer.material = ColorSettings.Instance.GetMaterial(color);
        }
    }
}
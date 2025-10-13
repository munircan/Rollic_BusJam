using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonModelController : ComponentModule<Person>
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        internal override void Initialize()
        {
            base.Initialize();
            SetColor();
        }

        public void SetColor()
        {
            _meshRenderer.material = ColorSettings.Instance.GetPersonMaterial(BaseComp.Data.Color,BaseComp.Data.Type,false);
        }

        public void SetCanPressableColor()
        {
            _meshRenderer.material = ColorSettings.Instance.GetPersonMaterial(BaseComp.Data.Color,BaseComp.Data.Type,true);
        }
    }
}   
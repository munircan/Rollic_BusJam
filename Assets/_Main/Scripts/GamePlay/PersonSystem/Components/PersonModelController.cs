using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonModelController : ComponentModule<Person>
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Outline _outline;

        internal override void Initialize()
        {
            base.Initialize();
            SetColor();
        }

        public void SetColor()
        {
            _meshRenderer.material = ColorSettings.Instance.GetPersonMaterial(BaseComp.Data.colorType,BaseComp.Data.Appearance,false);
        }


        public void SetOutlineEnable(bool value)
        {
            _outline.enabled = value;
        }

        internal override void Reset()
        {
            base.Reset();
            SetOutlineEnable(false);
        }
    }
}   
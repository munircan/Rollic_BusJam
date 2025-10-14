using UnityEngine;

namespace _Main.Scripts.Patterns.ModuleSystem
{
    public abstract class ComponentModule<TBaseComponent> : MonoBehaviour where TBaseComponent : BaseComponent
    {
        protected TBaseComponent BaseComp;
        private bool _hasInitialized = false;

        public virtual void Initialize()
        {
            if (_hasInitialized) 
                return;

            BaseComp = GetComponent<TBaseComponent>();
            _hasInitialized = true;
        }

        public virtual void Reset()
        {
        }
    }
}
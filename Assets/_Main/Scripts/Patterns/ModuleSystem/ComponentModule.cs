using UnityEngine;

namespace _Main.Patterns.ModuleSystem
{
    public abstract class ComponentModule<TBaseComponent> : MonoBehaviour where TBaseComponent : BaseComponent
    {
        protected TBaseComponent BaseComp;
        protected bool HasInitialized = false;

        internal virtual void Initialize()
        {
            if (HasInitialized)
                return;

            BaseComp = GetComponent<TBaseComponent>();
            HasInitialized = true;
        }

        internal virtual void Reset()
        {
        }
    }
}
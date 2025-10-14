using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.Patterns.Singleton
{
    public abstract class SingletonScriptableObject : ScriptableObject
    {
        public abstract void Initialize();

        public abstract void Terminate();
    }

    public abstract class SingletonScriptableObject<T> : SingletonScriptableObject where T : SingletonScriptableObject
    {
        [HideInEditorMode] [SerializeField] private T _instance;

        protected static object _lock = new object();
        public static T Instance;
        protected bool _hasInstance = false;

        public override void Initialize()
        {
            if (!_hasInstance)
                CreateInstance();
        }

        public override void Terminate()
        {
            if (_hasInstance)
                DestroyInstance();
        }

        protected void CreateInstance()
        {
            lock (_lock)
            {
                if (_instance != null)
                    return;

                var clone = Instantiate(this);
                _instance = clone as T;
                Instance = clone as T;
                _hasInstance = true;
            }
        }

        protected void DestroyInstance()
        {
            if (_instance != null)
                Destroy(_instance);

            _instance = null;
            Instance = null;
            _hasInstance = false;
        }
    }
}
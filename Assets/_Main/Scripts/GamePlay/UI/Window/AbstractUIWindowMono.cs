using UnityEngine;

namespace _Main.Scripts.GamePlay.UI.Window
{
    public abstract class AbstractUIWindowMono : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        
        public abstract UIWindowType UIWindowType { get; }

        #region InitializationMethods

        public virtual void Initialize()
        {
            
        }

        public virtual void Terminate()
        {
            
        }

        #endregion
    }
}
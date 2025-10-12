using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.GamePlay.UI.Window
{
    public abstract class AbstractUIWindowMono : MonoBehaviour
    {
        [FoldoutGroup("Basics")]
        [SerializeField] protected Canvas _canvas;
        [FoldoutGroup("Basics")]
        [SerializeField] protected CanvasGroup _canvasGroup;
        [FoldoutGroup("Basics")] 
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
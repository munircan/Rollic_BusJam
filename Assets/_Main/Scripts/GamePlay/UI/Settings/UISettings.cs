using System;
using System.Collections.Generic;
using _Main.Patterns.Singleton;
using _Main.Scripts.GamePlay.UI.Window;
using UnityEngine;

namespace _Main.Scripts.GamePlay.UI.Settings
{
    [CreateAssetMenu(fileName = "UISettings", menuName = "Settings/UI Settings", order = 0)]
    public class UISettings : SingletonScriptableObject<UISettings>
    {
        [SerializeField] private UIWindowInfo[] _uiWindowSOArray;
        

        #region HelperMethods

        public UIWindowInfo GetWindowInfo(Type type)
        {
            var arrayLenght = _uiWindowSOArray.Length;
            for (int i = 0; i < arrayLenght; i++)
            {
                if (_uiWindowSOArray[i].WindowPrefab.GetType() != type)
                    continue;

                return _uiWindowSOArray[i];
            }
            
            Debug.LogError("Window Info you trying to get is not added to info array!" + nameof(type));
            return null;
        }

        public UIWindowInfo GetWindowInfo(UIWindowType windowType)
        {
            var arrayLenght = _uiWindowSOArray.Length;
            for (int i = 0; i < arrayLenght; i++)
            {
                if (_uiWindowSOArray[i].UIWindowType != windowType)
                    continue;

                return _uiWindowSOArray[i];
            }
            
            Debug.LogError("Window Info you trying to get is not added to info array! " + windowType);
            return null;
        }

        #endregion
    }
}
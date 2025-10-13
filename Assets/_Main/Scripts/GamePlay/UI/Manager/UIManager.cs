using System.Collections.Generic;
using _Main.Patterns.Singleton;
using _Main.Scripts.GamePlay.UI.Settings;
using _Main.Scripts.GamePlay.UI.Window;
using UnityEngine;

namespace _Main.Scripts.GamePlay.UI.Manager
{
    public class UIManager : MonoBehaviour
    {
        private List<AbstractUIWindowMono> _liveUIWindowList = new List<AbstractUIWindowMono>();
        [SerializeField] private RectTransform _uiWindowContainer;


        private void Start()
        {
            InitializeInGameWindow();
        }

        private void InitializeInGameWindow()
        {
            GetWindow<UIWindowMonoInGame>();
        }


        public bool IsWindowExist<T>() where T : AbstractUIWindowMono
        {
            var liveUIWindowCount = _liveUIWindowList.Count;
            for (int i = 0; i < liveUIWindowCount; i++)
            {
                if (_liveUIWindowList[i].GetType() != typeof(T)) 
                    continue;

                return true;
            }
            
            return false;
        }
        
        
        public T GetWindow<T>() where T : AbstractUIWindowMono
        {
            T windowMono = null;
            var isWindowExist = false;
            
            var liveUIWindowCount = _liveUIWindowList.Count;
            for (int i = 0; i < liveUIWindowCount; i++)
            {
                if (_liveUIWindowList[i].GetType() != typeof(T)) 
                    continue;
                isWindowExist = true;
                windowMono = (T)_liveUIWindowList[i];
                break;
            }

            if (!isWindowExist)
            {
                windowMono = (T)CreateWindow(UISettings.Instance.GetWindowInfo(typeof(T)));
                _liveUIWindowList.Add(windowMono);
            }

            return windowMono;
        }
        
        public void CloseWindow<T>() where T : AbstractUIWindowMono
        {
            var isWindowExist = IsWindowExist<T>();
            if (!isWindowExist)
                return;

            var windowMono = GetWindow<T>();
            _liveUIWindowList.Remove(windowMono);
            windowMono.Terminate();
            Destroy(windowMono);
        }
        
        private AbstractUIWindowMono CreateWindow(UIWindowInfo windowInfo)
        {
            var createdWindowMono = Instantiate(windowInfo.WindowPrefab, _uiWindowContainer);
            createdWindowMono.Initialize();
            return createdWindowMono;
        }
    }
}
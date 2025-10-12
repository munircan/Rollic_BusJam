using UnityEngine;

namespace _Main.Scripts.GamePlay.UI.Window
{
    [CreateAssetMenu(fileName = "UI Window Info", menuName = "Window/Info", order = 0)]
    public class UIWindowInfo : ScriptableObject
    {
        [SerializeField] protected UIWindowType _uiWindowType;
        [SerializeField] protected AbstractUIWindowMono _windowPrefab;

        public UIWindowType UIWindowType => _uiWindowType;

        public AbstractUIWindowMono WindowPrefab => _windowPrefab;
    }
}
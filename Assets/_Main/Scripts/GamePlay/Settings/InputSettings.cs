using _Main.Patterns.Singleton;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Settings/Input Settings", order = 0)]
    public class InputSettings : SingletonScriptableObject<InputSettings>
    {
        [SerializeField] private float _holdDuration;
        [SerializeField] private LayerMask _layerMask;

        
        
    }
}
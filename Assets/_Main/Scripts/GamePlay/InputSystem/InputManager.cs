using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private bool _hasInitialized;
        private PressableInputHandler  _pressableInputHandler;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _pressableInputHandler = new PressableInputHandler(Camera.main);
            _hasInitialized = true;
        }


        private void Update()
        {
            if (!_hasInitialized || GameConfig.State != GameState.Playing)
            {
                return;
            }
            
            _pressableInputHandler.Cast();
        }
    }
}
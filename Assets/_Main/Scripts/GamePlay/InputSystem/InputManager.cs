using System;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.GamePlay.InputSystem.Handlers;
using _Main.Scripts.GamePlay.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private bool _hasInitialized;
        private PressableInputHandler  _pressableInputHandler;

        private void OnEnable()
        {
            GameConfig.MainCamera = Camera.main;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _pressableInputHandler = new PressableInputHandler(GameConfig.MainCamera);
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
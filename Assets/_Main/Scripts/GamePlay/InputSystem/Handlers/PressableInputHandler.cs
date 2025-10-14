using _Main.Scripts.GamePlay.InputSystem.Interactables;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.Utilities;
using UnityEngine;

namespace _Main.Scripts.GamePlay.InputSystem.Handlers
{
    public class PressableInputHandler : IInputHandler
    {
        private InputSettings _settings;
        private Camera _mainCamera;

        private RaycastHit _hit;
        private bool _isHandling;
        private IPressable _handlingInteractable;

        bool IInputHandler.IsHandling
        {
            get => _isHandling;
            set => _isHandling = value;
        }

        public void Cast()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity,
                        _settings.LayerMask))
                {
                    return;
                }


                if (_hit.collider.attachedRigidbody &&
                    _hit.collider.attachedRigidbody.TryGetComponent(out IPressable pressable))
                {
                    StartHandle(pressable);
                }
            }
            else if (Input.GetMouseButtonUp(0) && _isHandling)
            {
                StopHandle();
            }
        }

        public void StartHandle(IInteractable interactable)
        {
            if (_isHandling)
                return;

            _handlingInteractable = (IPressable)interactable;
            _handlingInteractable.OnPressDown();
            _isHandling = true;
        }
        
        public void StopHandle()
        {
            if (!_isHandling)
                return;

            _handlingInteractable.OnPressUp();
            _isHandling = false;
            GameConfig.LevelClickCount++;
        }

        public PressableInputHandler(Camera mainCamera)
        {
            _settings = InputSettings.Instance;
            _mainCamera = mainCamera;
        }
    }
}
using System;
using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.CustomEvents;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.GamePlay.UI.Window
{
    public class UIWindowMonoInGame : AbstractUIWindowMono
    {
        [FoldoutGroup("Level Over Panels")] [SerializeField]
        private LevelOverPanel _levelSuccessPanel;

        [FoldoutGroup("Level Over Panels")] [SerializeField]
        private LevelOverPanel _levelFailPanel;

        [FoldoutGroup("Level")] [ChildGameObjectsOnly] [SerializeField]
        private TextMeshProUGUI _levelText;

        [FoldoutGroup("Settings")] [ChildGameObjectsOnly] [SerializeField]
        private Button _restartLevelButton;


        private readonly int h_trigger_idle = Animator.StringToHash("trigger_idle");
        private readonly int h_trigger_success = Animator.StringToHash("trigger_success");
        private readonly int h_trigger_fail = Animator.StringToHash("trigger_fail");


        #region EncapsulationMethods

        public override UIWindowType UIWindowType => UIWindowType.InGameUI;

        #endregion

        #region Unity Events

        private void Awake()
        {
            EventManager.Subscribe<EventLevelLoad>(OnLevelLoad);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventLevelLoad>(OnLevelLoad);
        }


        #endregion
 

        #region InitializationMethods

        public override void Initialize()
        {
            base.Initialize();
            EventManager.Subscribe<EventLevelSuccess>(OnLevelSuccess);
            EventManager.Subscribe<EventLevelFail>(OnLevelFail);

            _levelSuccessPanel.TapButton.onClick.AddListener(OnClickLevelSuccessPanel);
            _levelFailPanel.TapButton.onClick.AddListener(OnClickLevelFailPanel);

            _restartLevelButton.onClick.AddListener(OnClickRestartLevel);

            _animator.SetTrigger(h_trigger_idle);
            UpdateLevelUI();
        }

        public override void Terminate()
        {
            base.Terminate();

            EventManager.Unsubscribe<EventLevelSuccess>(OnLevelSuccess);
            EventManager.Unsubscribe<EventLevelFail>(OnLevelFail);

            _levelSuccessPanel.TapButton.onClick.RemoveListener(OnClickLevelSuccessPanel);
            _levelFailPanel.TapButton.onClick.RemoveListener(OnClickLevelFailPanel);

            _restartLevelButton.onClick.RemoveListener(OnClickRestartLevel);
        }

        #endregion


        #region UIMethods

        private void UpdateLevelUI()
        {
            _levelText.text = Keys.Level + " " + LevelSettings.Instance.CurrentLevel;
        }

        #endregion

        #region OnClickMethods

        private void OnClickLevelSuccessPanel()
        {
            LevelManager.Instance.RefreshAndLoadLevel();
        }

        private void OnClickLevelFailPanel()
        {
            LevelManager.Instance.RefreshAndLoadLevel();
        }

        private void OnClickRestartLevel()
        {
            LevelManager.Instance.RefreshAndLoadLevel();
        }

        private void OnLevelSuccess(EventLevelSuccess customEvent)
        {
            _animator.SetTrigger(h_trigger_success);
        }

        private void OnLevelFail(EventLevelFail customEvent)
        {
            _animator.SetTrigger(h_trigger_fail);
        }

        #endregion
        
        
        
        private void OnLevelLoad(EventLevelLoad customEvent)
        {
            _animator.SetTrigger(h_trigger_idle);
            UpdateLevelUI();
        }

    }

    [Serializable]
    public struct LevelOverPanel
    {
        public GameObject LevelOverPanelObject;
        public Button TapButton;
    }
}
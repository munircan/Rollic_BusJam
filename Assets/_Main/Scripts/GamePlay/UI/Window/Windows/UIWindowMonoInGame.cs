using System;
using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.CustomEvents.LevelEvents;
using _Main.Scripts.GamePlay.GameStateSystem;
using _Main.Scripts.GamePlay.Helpers;
using _Main.Scripts.GamePlay.LevelSystem.Manager;
using _Main.Scripts.GamePlay.Settings;
using _Main.Scripts.GamePlay.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.GamePlay.UI.Window.Windows
{
    public class UIWindowMonoInGame : AbstractUIWindowMono
    {
        #region SerializedFields

        [FoldoutGroup("Level Over Panels")] [SerializeField]
        private LevelOverPanel _tapToStartPanel;

        [FoldoutGroup("Level Over Panels")] [SerializeField]
        private LevelOverPanel _levelSuccessPanel;

        [FoldoutGroup("Level Over Panels")] [SerializeField]
        private LevelOverPanel _levelFailPanel;

        [FoldoutGroup("Level")] [ChildGameObjectsOnly] [SerializeField]
        private TextMeshProUGUI _levelText;

        [FoldoutGroup("Level")] [ChildGameObjectsOnly] [SerializeField]
        private TextMeshProUGUI _timerText;

        [FoldoutGroup("Settings")] [ChildGameObjectsOnly] [SerializeField]
        private Button _restartLevelButton;

        #endregion
        
        #region Animation Hashes

        private readonly int h_trigger_tap_to_start = Animator.StringToHash("trigger_start");
        private readonly int h_trigger_idle = Animator.StringToHash("trigger_idle");
        private readonly int h_trigger_success = Animator.StringToHash("trigger_success");
        private readonly int h_trigger_fail = Animator.StringToHash("trigger_fail");

        #endregion
        
        #region Timer Variables

        private Timer _timer;
        private const float TIMER_INTERVAL = 1;

        #endregion
        
        #region EncapsulationMethods

        public override UIWindowType UIWindowType => UIWindowType.InGameUI;

        #endregion

        #region Unity Events

        private void Awake()
        {
            EventManager.Subscribe<EventLevelLoaded>(OnLevelLoad);
        }


        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventLevelLoaded>(OnLevelLoad);
        }

        #endregion


        #region InitializationMethods

        public override void Initialize()
        {
            base.Initialize();
            EventManager.Subscribe<EventLevelSuccess>(OnLevelSuccess);
            EventManager.Subscribe<EventLevelFail>(OnLevelFail);

            _tapToStartPanel.TapButton.onClick.AddListener(OnClickTapToStartButton);
            _levelSuccessPanel.TapButton.onClick.AddListener(OnClickLevelSuccessPanel);
            _levelFailPanel.TapButton.onClick.AddListener(OnClickLevelFailPanel);

            _restartLevelButton.onClick.AddListener(OnClickRestartLevel);
            _timer = new Timer();
            _animator.SetTrigger(h_trigger_tap_to_start);
            UpdateLevelUI();
        }

        public override void Terminate()
        {
            base.Terminate();

            EventManager.Unsubscribe<EventLevelSuccess>(OnLevelSuccess);
            EventManager.Unsubscribe<EventLevelFail>(OnLevelFail);

            _tapToStartPanel.TapButton.onClick.RemoveListener(OnClickTapToStartButton);
            _levelSuccessPanel.TapButton.onClick.RemoveListener(OnClickLevelSuccessPanel);
            _levelFailPanel.TapButton.onClick.RemoveListener(OnClickLevelFailPanel);

            _restartLevelButton.onClick.RemoveListener(OnClickRestartLevel);
        }

        #endregion
        
        #region UIMethods

        private void UpdateLevelUI()
        {
            _levelText.text = Keys.LEVEL + " " + LevelSettings.Instance.GetCurrentLevelName();
            UpdateTimerText(LevelSettings.Instance.GetCurrentLevelData().LevelDuration);
        }

        private void UpdateTimerText(int time)
        {
            _timerText.text = time.ToString();
        }

        #endregion

        #region OnClickMethods

        private void OnClickTapToStartButton()
        {
            _animator.SetTrigger(h_trigger_idle);
            EventManager.Publish(EventLevelStart.Create());
            StartTimer();
        }


        private void OnClickLevelSuccessPanel()
        {
            EventManager.Publish(EventLoadLevel.Create());
        }

        private void OnClickLevelFailPanel()
        {
            EventManager.Publish(EventLoadLevel.Create());
        }

        private void OnClickRestartLevel()
        {
            StopTimer();
            EventManager.Publish(EventLoadLevel.Create());
        }

        private void OnLevelSuccess(EventLevelSuccess customEvent)
        {
            StopTimer();
            _animator.SetTrigger(h_trigger_success);
        }

        private void OnLevelFail(EventLevelFail customEvent)
        {
            StopTimer();
            _animator.SetTrigger(h_trigger_fail);
        }

        #endregion

        #region Event Methods

        private void OnLevelLoad(EventLevelLoaded customEvent)
        {
            _animator.SetTrigger(h_trigger_tap_to_start);
            UpdateLevelUI();
        }

        #endregion

        #region Timer

        private void StartTimer()
        {
            var levelDuration = LevelSettings.Instance.GetCurrentLevelData().LevelDuration;
            _timer.Initialize(levelDuration, TIMER_INTERVAL, OnTick, OnTimerComplete);
            _timer.StartTimer();
        }

        private void StopTimer()
        {
            _timer.StopTimer();
        }

        private void OnTick(float time)
        {
            UpdateTimerText((int)time);
        }

        private void OnTimerComplete()
        {
            GameConfig.FailReason = FailReason.Timeout;
            LevelManager.LevelFailed();
        }

        #endregion
    }

    [Serializable]
    public struct LevelOverPanel
    {
        public GameObject LevelOverPanelObject;
        public Button TapButton;
    }
}
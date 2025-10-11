using System;
using UnityEngine;

namespace _Main.Scripts.Utilities
{
    public class Timer
    {
        private bool _isPaused;
        private float _timer;
        private readonly float _duration;


        private Action _onTimeEnded;

        public Timer(float duration, Action onComplete = null)
        {
            _duration = duration;
            InitializeTimer(onComplete);
        }


        private void InitializeTimer(Action onComplete)
        {
            _isPaused = false;
            _timer = _duration;
            _onTimeEnded = onComplete;
        }


        public void CountTimer()
        {
            if (_isPaused)
                return;

            if (_timer <= -.01f)
            {
                _timer += _duration;
                _onTimeEnded?.Invoke();
            }


            _timer -= Time.deltaTime;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void ResetTimer()
        {
            _timer = _duration;
            _isPaused = false;
        }
    }
}
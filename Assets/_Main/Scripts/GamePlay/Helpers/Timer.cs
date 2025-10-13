using System;
using System.Collections.Generic;
using MEC;

namespace _Main.Scripts.GamePlay.Helpers
{
    public class Timer
    {
        private float _timer;
        private float _interval;
        private CoroutineHandle _coroutineHandle;


        private Action<float> _onTick;
        private  Action _onComplete;
        

        public void Initialize(float duration, float interval = 1, Action<float> onTick = null,Action onComplete = null)
        {
            _timer = duration;
            _interval = interval;
            _onTick = onTick;
            _onComplete = onComplete;
        }

        public void StartTimer()
        {
            _coroutineHandle = Timing.RunCoroutine(CounterCoroutine());
        }

        private IEnumerator<float> CounterCoroutine()
        {
            while (_timer > 0)
            {
                yield return Timing.WaitForSeconds(_interval);
                _timer -= _interval;
                _onTick?.Invoke(_timer);
            }
            
            _onComplete?.Invoke();
        }

        public void StopTimer()
        {
            Timing.KillCoroutines(_coroutineHandle);
        }
    }
}
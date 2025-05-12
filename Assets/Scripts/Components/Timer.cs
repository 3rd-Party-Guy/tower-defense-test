using System;

namespace TDTest.Time
{
    public class Timer : IDisposable
    {
        public event Action OnStart;
        public event Action OnPause;
        public event Action OnResume;
        public event Action OnToggle;
        public event Action OnTick;
        public event Action OnComplete;
        public event Action OnRepeatStart;

        public bool IsRunning => (Time > 0f);

        public bool IsRepeating { get; set; } = false;
        public bool IsScaled { get; set; } = true;
        public bool IsPaused { get; private set; } = true;
        public float Time { get; private set; } = 0f;
        public float Duration { get; private set; } = 0f;

        bool disposed = false;

        #region Constuct/Deconstruct
        public Timer()
        {
            Statics.Time.Register(this);
        }

        ~Timer()
        {
            Dispose(false);
        }
        #endregion

        #region Toggles
        public void Start(float duration)
        {
            Duration = duration;
            Time = duration;
            IsPaused = false;
            OnStart?.Invoke();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime)
        {
            if (IsPaused) return;

            Time -= (IsScaled) ? deltaTime : unscaledDeltaTime;
            if (Time <= 0f)
            {
                OnComplete?.Invoke();

                if (IsRepeating)
                {
                    Time = Duration;
                    OnRepeatStart?.Invoke();
                }
                else
                {
                    IsPaused = true;
                }
            }

            OnTick?.Invoke();
        }

        public void Pause()
        {
            IsPaused = true;
            OnPause?.Invoke();
        }

        public void Resume()
        {
            IsPaused = false;
            OnResume?.Invoke();
        }

        public void Toggle()
        {
            IsPaused = !IsPaused;
            var specificEvent = (IsPaused) ? OnPause : OnResume;

            specificEvent?.Invoke();
            OnToggle?.Invoke();
        }
        #endregion

        #region Dispose Implementation
        public void Dispose()
        {
            Dispose(true);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                CleanupManaged();
            }

            CleanupUnmanaged();
            disposed = true;
        }

        private void CleanupManaged()
        {
                Statics.Time.Unregister(this);

                OnStart = null;
                OnPause = null;
                OnResume = null;
                OnToggle = null;
                OnTick = null;
                OnComplete = null;
                OnRepeatStart = null;
        }

        private void CleanupUnmanaged() { }
        #endregion
    }
}

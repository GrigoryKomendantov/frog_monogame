using System;
using System.Timers;

namespace Frog.Engine
{
    public enum Status
    {
        Stopped,
        Running
    };

    public class Timer
    {
        private GameObject _gameObject;
        private System.Timers.Timer _timer;

        public DateTime DateTime { get; set; }

        public Single Elapsed
        {
            get
            {
                return (Single)DateTime.Now.Subtract(DateTime).TotalMilliseconds;
            }
        }

        public delegate void OnTimerHandler(Single elapsed);
        public event OnTimerHandler OnTimer;
        
        public Status Status { get; private set; }
        
        
        public Timer(GameObject gameObject, Single delay)
        {
            _gameObject = gameObject;

            _timer = new System.Timers.Timer(delay);
            _timer.AutoReset = true;
            _timer.Elapsed += TimerTick;


            Status = Status.Stopped;
            DateTime = DateTime.Now;
        }

        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            OnTimer(Elapsed);
            
            DateTime = DateTime.Now;
        }


        public void Start()
        {
            _timer.Start();
            Status = Status.Running;
        }
        public void Stop()
        {
            _timer.Stop();
            Status = Status.Stopped;
        }
    }
}

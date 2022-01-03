using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BinarySphere
{
    public partial class Core
    {
        private bool CloseRequest { get; set; } = false;
        private bool IsRunning { get; set; } = false;
        public long Delay { get; private set; } = 20;
        public long FPS { get; private set; } = 50;

        public event Action Initializing;
        public event Action Initialized;
        public event Action Terminating;
        public event Action Terminated;

        public Action Routine;

        public Stopwatch Timer = new Stopwatch();

        public Core()
        {
            IsRunning = false;
            CloseRequest = false;
        }

        public void SetFPS(long fps)
        {
            FPS = fps;
            Delay = 1000 / fps;
        }

        public override string ToString()
        {
            return "FPS: " + FPS;
        }

        public void Initialize()
        {
            Initializing?.Invoke();
            Initialized?.Invoke();
            Run();
        }

        public void Terminate()
        {
            Terminating?.Invoke();

            CloseRequest = true;

            Terminated?.Invoke();
        }

        private async void Run()
        {
            while (CloseRequest)
            {
                Timer.Restart();

                IsRunning = true;

                while (Timer.ElapsedMilliseconds < FPS)
                {
                    await Task.Delay(1);
                }

                Routine?.Invoke();

                IsRunning = false;
            }
        }
    }
}

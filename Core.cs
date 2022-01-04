using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BinarySphere
{
    public partial class Core
    {
        private bool CloseRequest { get; set; } = false;

        private Stopwatch Timer = new Stopwatch();

        public bool IsInitialized { get; private set; } = false;
        public bool IsTerminated { get; private set; } = false;


        public long Delay { get; private set; } = 20;
        public long FPS { get; private set; } = 50;

        public event Action Initializing;
        public event Action Initialized;
        public event Action Terminating;
        public event Action Terminated;

        public Action Routine;

        public Core()
        {
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

            IsInitialized = true;
        }

        public void Terminate()
        {
            Terminating?.Invoke();

            CloseRequest = true;

            Terminated?.Invoke();
        }

        public async void Run()
        {
            IsTerminated = false;

            while (!CloseRequest)
            {
                Timer.Restart();

                while (Timer.ElapsedMilliseconds < Delay)
                {
                    await Task.Delay(1);
                }

                Routine?.Invoke();
            }

            IsTerminated = true;
        }
    }
}
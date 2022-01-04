using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace BinarySphere
{
    public class Object
    {
        public Point Location { get; protected set; } = new Point(0, 0);

        public Size Size { get; protected set; } = new Size(50, 50);

        protected string CurrentState { get; set; }

        protected Color BackgroundColor { get; set; } = Color.Green;

        private Dictionary<string, AnimationClip> AnimationClips { get; set; } = new Dictionary<string, AnimationClip>();

        protected Object(string state, AnimationClip clip)
        {
            CurrentState = state;

            AddAnimationClip(state, clip);
        }

        private async void WaitAnimationFrame(long fps)
        {
            var timer = new Stopwatch();

            timer.Start();

            while (timer.ElapsedMilliseconds < fps * AnimationClips[CurrentState].GetCurrentImageFrame())
            {
                await Task.Delay(1);
            }

            timer.Stop();
        }

        public Bitmap Draw(long fps)
        {
            var result = new Bitmap(Size.Width, Size.Height);
            var gfx = Graphics.FromImage(result);

            BackgroundColor = Color.FromArgb(new Random().Next(0, 256), new Random().Next(0, 256), new Random().Next(0, 256), new Random().Next(0, 256));
            gfx.Clear(BackgroundColor);

            gfx.DrawImage(AnimationClips[CurrentState].GetCurrentImage(), new Point(0, 0));

            WaitAnimationFrame(fps);

            return result;
        }

        public void AddAnimationClip(string state, AnimationClip clip)
        {
            AnimationClips.Add(state, clip);
        }
    }
}

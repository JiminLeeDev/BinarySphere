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

        protected string CurrentState { get; set; } = "Idle";

        protected Color BackgroundColor { get; set; } = Color.Green;

        private Dictionary<string, AnimationClip> AnimationClips { get; set; } = new Dictionary<string, AnimationClip>();

        protected Object()
        {
            AddAnimationClip(CurrentState, new AnimationClip(new int[] { }, new Bitmap[] { }));
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

        protected Graphics Draw(long fps)
        {
            var result = Graphics.FromImage(new Bitmap(Size.Width, Size.Height));

            result.Clear(BackgroundColor);

            result.DrawImage(AnimationClips[CurrentState].GetCurrentImage(), Location);

            WaitAnimationFrame(fps);

            return result;
        }


        public void AddAnimationClip(string state, AnimationClip clip)
        {
            AnimationClips.Add(state, clip);
        }
    }
}

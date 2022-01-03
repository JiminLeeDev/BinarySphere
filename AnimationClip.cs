using System.Collections.Generic;
using System.Drawing;

namespace BinarySphere
{
    public class AnimationClip
    {
        private int ClipLength { get; set; }

        private int CurrentImageIdx { get; set; }

        private int[] Frames;

        private Bitmap[] Images;

        public AnimationClip(int[] frames, Bitmap[] images)
        {
            ClipLength = frames.Length;

            CurrentImageIdx = 0;

            Frames = frames;

            Images = images;
        }

        public void ChangeCurImage()
        {
            if (CurrentImageIdx >= ClipLength - 1)
            {
                CurrentImageIdx = 0;
            }
            else
            {
                CurrentImageIdx++;
            }
        }

        public Bitmap GetCurrentImage() =>
            Images[CurrentImageIdx];

        public int GetCurrentImageFrame() =>
            Frames[CurrentImageIdx];
    }
}

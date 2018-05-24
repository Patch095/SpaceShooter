using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using System.Xml;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Animation : IUpdatable, IActivable, ICloneable
    {
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        float frameDelay;
        int currentFrameIndex;
        public int CurrentFrame
        {
            get { return currentFrameIndex; }
            private set
            {
                currentFrameIndex = value;
                OffsetX = startX + (CurrentFrame % cols) * FrameWidth;
                OffsetY = startY + (CurrentFrame / cols) * FrameWidth;
            }
        }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        int rows;
        int cols;
        int numFrames;

        int startX;
        int startY;

        float counter;
        bool loop;

        public bool IsActive { get; set; }

        public Animation(int frameW, int frameH, int c = 1, int r = 1, float fps = 1f, bool l = true, int sX = 0, int sY = 0)
        {
            FrameHeight = frameH;
            FrameWidth = frameW;
            loop = l;
            rows = r;
            cols = c;
            startX = sX;
            startY = sY;
            frameDelay = 1 / fps;
            numFrames = rows * cols;
            IsActive = true;
            CurrentFrame = 0;
        }

        public void Update()
        {
            if (IsActive)
            {
                counter += Game.DeltaTime;
                if (counter >= frameDelay)
                {
                    counter = 0;
                    if(++CurrentFrame == numFrames)
                        OnAnimationEnds();
                }
            }
        }
        private void OnAnimationEnds()
        {
            if (!loop)
                IsActive = false;
            else
                CurrentFrame = 0;
        }

        public object Clone()
        {
            //Deep Copy, copia tutti gli oggeti al interno
            return this.MemberwiseClone();//Swallow Copy, copia la reference
        }
    }
}
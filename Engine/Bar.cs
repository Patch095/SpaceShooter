using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Bar : GameObject
    {
        protected float value;
        protected float barWidth;
        protected Sprite frame;
        protected Texture frameTexture;

        /*protected float startValue;
        protected float finalValue;
        protected float counter;*/
        public float Delay { get; set; }

        public float MaxValue { get; set; }

        public Vector2 BarOffset
        {
            set
            {
                sprite.position = frame.position + value;
            }
        }

        public Bar(Vector2 position, string textureName = "playerBar", float maxValue = 100, int height = 0) : base(position, textureName, DrawManager.Layer.GUI)
        {
            barWidth = texture.Width;
            value = MaxValue = maxValue;

            frameTexture = GfxManager.GetSpritesheet("barFrame").Item1;
            frame = new Sprite(frameTexture.Width, frameTexture.Height)
            {
                position = position
            };
        }

        public virtual void SetValue(float newValue)
        {
            value = newValue;
            ResizeBar();
        }
        /*public virtual void SetValue(float newValue)
        {
            startValue = value;
            finalValue = newValue;
            counter = 0;
        }*/

        public void ResizeBar()
        {
            float scale = value / MaxValue;
            barWidth = texture.Width * scale;
            sprite.scale = new Vector2(scale, 1);
            sprite.SetAdditiveTint(1 - scale, scale - 1, scale - 1, 0);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                frame.DrawTexture(frameTexture);
                sprite.DrawTexture(texture, 0, 0, (int)barWidth, (int)sprite.Height);
            }
        }

        public void SetXPosition(float newX)
        {
            frame.position.X = newX;
            float xOffSet = newX - frame.position.X;

            sprite.position.X += xOffSet;
        }

        /*public override void Update()
        {
            base.Update();
            if (IsActive)
            {
                if(value != finalValue)
                {
                    counter += Game.DeltaTime;
                    if (counter < Delay)
                    {
                        float perc = counter / Delay;
                        value = finalValue - (startValue - finalValue) * perc;
                    }
                    else
                        value = finalValue;
                    ResizeBar();
                }
            }
        }*/
    }
}
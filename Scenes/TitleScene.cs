using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Lezione_53__22Gennaio2018_Ships
{
    class TitleScene : Scene
    {
        protected Sprite img;
        protected Texture imgTexture;

        //buttons
        protected float buttonPressWait;
        protected KeyCode nextSceneKey;
        //fade
        protected float counter;
        protected float counterDirection;
        protected float colorMul;

        public bool FadeIn { get; set; }
        public bool FadeOut { get; set; }
        public float FadeTime { get; set; }
        public float ShowTime { get; set; }
        
        public TitleScene(string texture, KeyCode keyNext)
        {
            FadeIn = true;
            FadeOut = true;
            FadeTime = 2.0f;
            ShowTime = 0;
            counterDirection = 0;
            nextSceneKey = keyNext;
            colorMul = 1;
            buttonPressWait = 0.6f;

            imgTexture = new Texture(texture);
            img = new Sprite(Game.Window.Width, Game.Window.Height);
        }

        public override void Start()
        {
            base.Start();
            if (FadeIn)
            {
                counter = 0;
                img.SetAdditiveTint(0, 0, 0, -1);
                counterDirection = 1;
                colorMul = 0;
            }
            else
            {
                counterDirection = 0;
                counter = ShowTime;
            }
        }

        public override void Input()
        {
            if (Game.Window.GetKey(nextSceneKey) && buttonPressWait <= 0)
            {
                if (FadeOut)
                {
                    if(counterDirection != -1)//fade in or show
                    {//go to fadeout
                        counter = FadeTime;
                        counterDirection = -1;
                    }
                }
                else
                    IsPlaying = false;
            }
        }

        public override void Update()
        {
            buttonPressWait -= Game.DeltaTime;
            if(counterDirection != 0)
            {
                counter += Game.DeltaTime * counterDirection;
                if(counterDirection > 0)
                {//Fade In
                    if(counter >= FadeTime)
                    {
                        counter = FadeTime;
                        counterDirection = 0;
                    }
                }
                else
                {
                    if(counter <= 0)
                    {//Fade Out
                        counter = 0;
                        IsPlaying = false;
                    }
                }
                colorMul = counter / FadeTime;

                if(counterDirection == 0)
                {//Show Image
                    counter = ShowTime;
                }
            }
            else
            {
                if(ShowTime != 0)
                {
                    counter -= Game.DeltaTime;
                    if(counter <= 0)
                    {
                        if (FadeOut)
                        {
                            counterDirection = -1;
                            counter = FadeTime;
                        }
                        else
                        {
                            IsPlaying = false;
                        }
                    }
                }
            }
            img.SetMultiplyTint(colorMul, colorMul, colorMul, 1);
        }

        public override void Draw()
        {
            img.DrawTexture(imgTexture);
        }
    }
}
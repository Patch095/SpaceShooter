using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Lezione_53__22Gennaio2018_Ships
{
    class GameOverScene : Scene
    {
        Sprite background;
        Texture bgTexture;

        public override void Start()
        {
            base.Start();
            bgTexture = new Texture("Assets/gameOverBg.png");
            background = new Sprite(Game.Window.Width, Game.Window.Height);
        }

        public override void Input()
        {
            if (Game.Window.GetKey(KeyCode.Y))
                IsPlaying = false;
            else if (Game.Window.GetKey(KeyCode.N))
            {
                IsPlaying = false;
                NextScene = null;
            }
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            background.DrawTexture(bgTexture);
        }
    }
}
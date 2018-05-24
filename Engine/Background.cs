using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Background : GameObject
    {
        protected Sprite bottomSprite;
        public float ScrollX { get; set; }

        public Background(string spritesheetName, Vector2 spritePosition, float scrollSpeed = 0) : base(spritePosition, spritesheetName, DrawManager.Layer.Background)
        {
            bottomSprite = new Sprite(texture.Width, texture.Height);
            ScrollX = scrollSpeed;
        }

        public override void Update()
        {
            if (IsActive)
            {
                sprite.position.X += ScrollX * Game.DeltaTime;

                bottomSprite.position.X = sprite.position.X + Width;

                if (sprite.position.X <= -Width)
                {
                    sprite.position.X = bottomSprite.position.X + Width;
                    Sprite first = sprite;
                    sprite = bottomSprite;
                    bottomSprite = first;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            bottomSprite.DrawTexture(texture);
        }
    }
}
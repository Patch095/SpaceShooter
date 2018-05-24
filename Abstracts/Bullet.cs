using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    abstract class Bullet : GameObject
    {
        protected Vector2 textureOffset;
        protected float dmg;
        public BulletManager.BulletType Type { get; protected set; }

        public Bullet(string spritesheetName = "bullets") : base(Vector2.Zero, spritesheetName, DrawManager.Layer.Playground)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
            IsActive = false;
            RigidBody = new RigidBody(sprite.position, this);
            dmg = 25;
        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, Width, Height);
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
            }
        }

        public virtual void Shoot(Vector2 startPos, Vector2 shootVelocity)
        {
            IsActive = true;
            Position = startPos;
            Velocity = shootVelocity;
        }

        public virtual void OnDie()
        {
            IsActive = false;
            //restore bullet
            BulletManager.RestoreBullet(this);
        }
    }
}
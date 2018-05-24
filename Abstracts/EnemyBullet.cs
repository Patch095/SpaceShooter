using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    abstract class EnemyBullet : Bullet
    {
        public EnemyBullet(string spritesheetName = "bullets") : base(spritesheetName)
        {
            RigidBody.Type = (uint)PhysicsManager.ColliderType.EnemyBullet;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            { 
                if (X + Width / 2 < 0 || Y - Height / 2 > Game.Window.Height + 300 || Y + Height / 2 <= -300)
                {
                    OnDie();
                }
            }
        }

        public override void Shoot(Vector2 startPos, Vector2 shootVelocity)
        {
            base.Shoot(startPos, shootVelocity);
        }

        public override void OnCollide(GameObject other)
        {
            if (other.IsActive)
            {
                if (other is Player)
                {
                    OnDie();
                    ((Player)other).AddDamage(dmg);
                }
                if (other is PlayerBullet)
                {
                    OnDie();
                    ((PlayerBullet)other).OnDie();
                }
            }
        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, Width, Height);
        }
    }
}
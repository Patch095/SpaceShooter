using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    abstract class PlayerBullet : Bullet
    {
        public PlayerBullet(string textureName = "bullets" ) : base(textureName)
        {
            RigidBody.Type = (uint)PhysicsManager.ColliderType.PlayerBullet;
            RigidBody.AddCollisionMask((uint)PhysicsManager.ColliderType.Enemy);
            RigidBody.AddCollisionMask((uint)PhysicsManager.ColliderType.EnemyBullet);
        }

        public override void Shoot(Vector2 startPos, Vector2 shootVelocity)
        {
            base.Shoot(startPos, shootVelocity);
        }

        public override void OnCollide(GameObject other)
        {
            if (other.IsActive)
            {
                if (other is EnemyShip)
                {
                    OnDie();
                    ((EnemyShip)other).AddDamage(dmg);
                }
                if(other is EnemyBullet)
                {
                    OnDie();
                    ((EnemyBullet)other).OnDie();
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            {
                if (X - Width / 2 >= Game.Window.Width || Y - Height / 2 >= Game.Window.Height || Y + Height / 2 < 0)
                {
                    OnDie();
                }
            }

        }
    }
}
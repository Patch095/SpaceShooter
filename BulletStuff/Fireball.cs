using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class FireBall : EnemyBullet
    {
        protected double elapsedTime;
        public FireBall(string spritesheetName = "fireGlobe") : base(spritesheetName)
        {
            Circle circle = new Circle(Vector2.Zero, null, sprite.Width / 2);
            RigidBody rb = new RigidBody(RigidBody.Position, this, circle, null, false)
            {
                Velocity = RigidBody.Velocity
            };
            rb.Type = (uint)PhysicsManager.ColliderType.EnemyBullet;
            rb.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);

            PhysicsManager.RemoveItem(RigidBody);
            RigidBody = rb;

            Type = BulletManager.BulletType.FIRE;
            dmg = 30;
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            {
                elapsedTime += Game.DeltaTime;
                RigidBody.SetYVelocity((float)Math.Cos(elapsedTime * 5) * 500);
                if (X + Width / 2 <= 0)
                    IsActive = false;
            }
        }

        public override void Shoot(Vector2 startPos, Vector2 shootVelocity)
        {
            base.Shoot(startPos, shootVelocity);
        }

        public override void OnCollide(GameObject other)
        {
            base.OnCollide(other);
        }
    }
}

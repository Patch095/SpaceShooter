using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class EnemyShip : Ship
    {
        public enum EnemyType { BONUS, BOT, RED, STANDAR }
        public EnemyType Type { get; protected set; }

        public EnemyShip(EnemyType t, string spritesheetName = "enemy",DrawManager.Layer drawLayer = DrawManager.Layer.Playground) : base(Vector2.Zero, spritesheetName, drawLayer)
        {
            this.Type = t;

            sprite.FlipX = true;
            sprite.scale = new Vector2(0.3f, 0.3f);
            Velocity = new Vector2(-200, 0);
            IsActive = false;
            cannonOffset = new Vector2(-Width / 2, 0);
            RigidBody = new RigidBody(sprite.position, this);

            float horizontalSpeed = -RandomGenerator.GetRandom(200, 350);
            RigidBody.Velocity = new Vector2(horizontalSpeed, 0);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Enemy;
            RigidBody.AddCollisionMask((uint)PhysicsManager.ColliderType.Player);

            MaxNrg = 1;
            Nrg = MaxNrg;
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                shootCounter -= Game.DeltaTime;
                if (shootCounter <= 0)
                {
                    Shoot();
                    shootCounter = RandomGenerator.GetRandom(1, 3);
                }
                if (Position.X + Width / 2 <= 0)
                    OnDie();
            }
        }

        public override bool Shoot()
        {
            Bullet b;
            b = BulletManager.GetBullet(BulletManager.BulletType.BLUE);

            if (b != null)
            {
                b.Shoot(Position + cannonOffset, new Vector2(Velocity.X - 350, 0));
                return true;
            }
            return false;
        }

        public void OnSpawn(Vector2 newPosition)
        {
            Position = newPosition;
            IsActive = true;
        }
        public override void OnCollide(GameObject other)
        {
            if (other.IsActive)
            {
                if (other is PlayerBullet)
                {
                    base.OnCollide(other);
                    OnDie();
                }
                if (other is Player)
                {
                    base.OnCollide(other);
                    ((Player)other).AddDamage(50);
                    OnDie();
                }
            }
        }
        public override void OnDie()
        {
            IsActive = false;
            SpawnManager.Restore(this);
            base.OnDie();
        }
    }
}
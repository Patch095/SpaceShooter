using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class PowerUp : EnemyShip
    {
        protected float time = 0;
        public int PowerValue;

        public PowerUp(EnemyType t, string spritesheetName = "bonus", DrawManager.Layer drawLayer = DrawManager.Layer.Playground) : base(t, spritesheetName, drawLayer)
        {
            sprite.scale = new Vector2(0.15f, 0.15f);

            float horizontalSpeed = -RandomGenerator.GetRandom(400, 700);
            RigidBody.Velocity = new Vector2(horizontalSpeed, 0);

            RigidBody.Type = (uint)PhysicsManager.ColliderType.PowerUp;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);
        }

        public override void OnCollide(GameObject other)
        {
            if(other is Player)
            {
                switch (PowerValue)
                {
                    case 1:
                        ((Player)other).GetShootPowerUp();
                        break;
                    case 2:
                        ((Player)other).GetSpeedPowerUp();
                        break;
                    default:
                        ((Player)other).AddDamage(-(RandomGenerator.GetRandom(1, 5) * 10));
                        break;
                }
                OnDie();
            }
        }

        public override void OnDie()
        {
            IsActive = false;
            SpawnManager.Restore(this);
        }

        public override bool Shoot()
        {
            return false;
        }

        public override void Update()
        {
            base.Update();
            time += Game.DeltaTime;
            RigidBody.SetYVelocity((float)Math.Sin(time * 20) * 250);
        }
    }
}

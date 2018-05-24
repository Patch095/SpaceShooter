using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class RedEnemyShip : EnemyShip
    {
        public RedEnemyShip(EnemyType t, string spritesheetName = "redEnemy", DrawManager.Layer drawLayer = DrawManager.Layer.Playground) : base(t, spritesheetName, drawLayer)
        {
            this.Type = t;
            
            float horizontalSpeed = -RandomGenerator.GetRandom(250, 500);
            RigidBody.Velocity = new Vector2(horizontalSpeed, 0);

            sprite.scale = new Vector2(0.5f, 0.5f);
        }

        public override bool Shoot()
        {
            int shootType = RandomGenerator.GetRandom(0, 10);
            if(shootType % 2 == 0)
                return base.Shoot();
            else
            {
                Bullet b;
                b = BulletManager.GetBullet(BulletManager.BulletType.FIRE);

                if (b != null)
                {
                    b.Shoot(Position - cannonOffset, new Vector2(Velocity.X - 300, 0));
                    return true;
                }
                return false;
            }
        }
    }
}

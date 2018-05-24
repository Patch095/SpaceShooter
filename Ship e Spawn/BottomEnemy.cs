using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class BottomEnemy : EnemyShip
    {
        protected Ship.ShootType bullet;

        public BottomEnemy(EnemyType t, string spritesheetName = "botEnemy", DrawManager.Layer drawLayer = DrawManager.Layer.Playground) : base(t, spritesheetName, drawLayer)
        {
            sprite.FlipX = true;
            this.Type = t;
            this.cannonOffset = new Vector2(0, Height / 3);

            float horizontalSpeed = -RandomGenerator.GetRandom(80, 220);
            RigidBody.Velocity = new Vector2(horizontalSpeed, 0);

            this.bullet = ShootType.DIAGONAL;
        }

        public override bool Shoot()
        {
            Bullet b;
            b = BulletManager.GetBullet(BulletManager.BulletType.BLUE);
            if (b != null)
            {
                if (bullet == ShootType.DIAGONAL)
                    b.Shoot(Position - cannonOffset, new Vector2(-200, -200));
                if (bullet == ShootType.VERTICAL)
                    b.Shoot(Position - cannonOffset, new Vector2(0, -350));
                ChangeBullets();
                return true;
            }
            return false;
        }
        public void ChangeBullets()
        {
            if (bullet == ShootType.DIAGONAL)
                bullet = ShootType.VERTICAL;
            else if (bullet == ShootType.VERTICAL)
                bullet = ShootType.DIAGONAL;
        }

        public override void OnDie()
        {
            IsActive = false;
            if (Position.X + Width / 2 > 0)
                new Explosion(new Vector2(Position.X - Width / 2, Position.Y - Height));
            SpawnManager.Restore(this);
        }
    }
}